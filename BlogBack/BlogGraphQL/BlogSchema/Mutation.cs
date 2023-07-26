using BlogBack.Services.Interfaces;
using DBRepository;
using DBRepository.Interfaces;
using DBRepository.Repositories;
using DevExpress.XtraRichEdit.Import.Html;
using GraphQL;
using GraphQL.Types;
using Models;
using Comment = Models.Comment;
using Tag = Models.Tag;

namespace BlogBack.BlogGraphQL.BlogSchema
{
    public class Mutation
    {
        [UseDbContext(typeof(RepositoryContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Post> AddPost([ScopedService] RepositoryContext context, string header, string body)
        {
            Post newPost = new Post { Header = header, Body = body, CreatedDate = DateTime.Now };
            context.Posts.Add(newPost);
            context.SaveChanges();
            return context.Posts.Where(x => x.PostId == newPost.PostId).AsQueryable();
        }

        [UseDbContext(typeof(RepositoryContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Post> DeletePost([ScopedService] RepositoryContext context, int id)
        {
            Post postsForDelete = new Post { PostId = id };
            context.Posts.Remove(postsForDelete);
            context.SaveChanges();
            return context.Posts.AsQueryable();
        }
    }
}
