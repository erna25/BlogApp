using BlogBack.BlogGraphQL.BlogTypes;
using BlogBack.Services.Interfaces;
using DBRepository;
using DBRepository.Interfaces;
using GraphQL;
using GraphQL.Types;
using Models;

namespace BlogBack.BlogGraphQL.BlogSchema
{
    public class Query
    {
        /// <summary>
        /// Get Posts
        /// </summary>
        /// <returns></returns>
        [UseDbContext(typeof(RepositoryContext))]
        public IQueryable<Post> GetAllPosts(RepositoryContext context) => context.Posts.AsQueryable();

    }
}
