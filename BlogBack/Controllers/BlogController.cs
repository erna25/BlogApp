using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.Authorization;
using BlogBack.Services.Interfaces;
using BlogBack.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogBack.Controllers
{
	[Route("api/[controller]")]
	public class BlogController : ControllerBase
    {
        IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

		[Route("page")]
		[HttpGet]
		public async Task<Page<PostLiteViewModel>> GetPosts(int pageIndex, string tag)
		{
			return await _blogService.GetPosts(pageIndex, tag);
		}

		[Route("post")]
		[HttpGet]
		public async Task<Post> GetPost(int postId)
		{
			return await _blogService.GetPost(postId);
		}

		[Route("comment")]
		[HttpPost]
		public async Task AddComment([FromBody] AddCommentRequest request)
		{
			await _blogService.AddComment(request);
		}

		[Route("comment")]
		[HttpDelete]
		public async Task DeleteComment(int commentId)
		{
			await _blogService.DeleteComment(commentId);
		}

		[Route("post")]
		[HttpPost]
		public async Task AddPost([FromBody] AddPostRequest request)
		{
			await _blogService.AddPost(request);
		}

		[Route("post")]
		[HttpDelete]
		public async Task DeletePost(int postId)
		{
			await _blogService.DeletePost(postId);
		}

		[Route("tags")]
		[HttpGet]
		public async Task<List<string>> GetTags()
		{
			return await _blogService.GetTags();
		}
	}
}