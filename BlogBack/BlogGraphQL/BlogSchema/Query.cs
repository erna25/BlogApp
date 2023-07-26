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
    public class Query
    {
        [UseDbContext(typeof(RepositoryContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Post> GetPosts([ScopedService] RepositoryContext context) => context.Posts.AsQueryable();

        [UseDbContext(typeof(RepositoryContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Post> GetPostById([ScopedService] RepositoryContext context, int id) => context.Posts.Where(x => x.PostId == id).AsQueryable();

        [UseDbContext(typeof(RepositoryContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Comment> GetComments([ScopedService] RepositoryContext context) => context.Comments.AsQueryable();

        [UseDbContext(typeof(RepositoryContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Tag> GetTags([ScopedService] RepositoryContext context) => context.Tags.AsQueryable();

        [UseDbContext(typeof(RepositoryContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<User> GetUsers([ScopedService] RepositoryContext context) => context.Users.AsQueryable();
    }
}
