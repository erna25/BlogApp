using DBRepository.Interfaces;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DBRepository.Repositories
{
	public class BlogRepository : BaseRepository, IBlogRepository
    {
		public BlogRepository(RepositoryContext context) : base(context) { }

		public async Task<Page<Post>> GetPosts(int index, int pageSize, string tag = null)
		{
			var result = new Page<Post>() { CurrentPage = index, PageSize = pageSize };

			var query = Context.Posts.AsQueryable();
			if (!string.IsNullOrWhiteSpace(tag))
			{
				query = query.Where(p => p.Tags.Any(t => t.TagName == tag));
			}

			result.TotalPages = await query.CountAsync();
			result.Records = await query.Include(p => p.Tags).Include(p => p.Comments).OrderByDescending(p => p.CreatedDate).Skip(index * pageSize).Take(pageSize).ToListAsync();


			return result;
		}

		public async Task<List<string>> GetAllTagNames()
		{
			return await Context.Tags.Select(t => t.TagName).Distinct().ToListAsync();
		}

		public async Task<Post> GetPost(int postId)
		{
			return await Context.Posts.Include(p => p.Tags).Include(p => p.Comments).FirstOrDefaultAsync(p => p.PostId == postId);
		}

		public async Task AddComment(Comment comment)
		{
			Context.Comments.Add(comment);
			Context.SaveChanges();
		}

		public async Task AddPost(Post post)
		{
			Context.Posts.Add(post);
			Context.SaveChanges();
		}

		public async Task DeletePost(int postId)
		{
			Post post = new Post() { PostId = postId };
			Context.Posts.Remove(post);
			Context.SaveChanges();
		}

		public async Task DeleteComment(int commentId)
		{
			Comment comment = new Comment() { CommentId = commentId };
			Context.Comments.Remove(comment);
			Context.SaveChanges();
		}
	}
}
