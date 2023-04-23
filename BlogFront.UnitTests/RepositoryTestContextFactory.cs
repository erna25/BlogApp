using DBRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UnitTests
{
    public class RepositoryTestContextFactory
    {
        public static RepositoryContext Create()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new RepositoryContext(options);
            context.Database.EnsureCreated();

            context.Posts.AddRange(
                new Post
                {
                    PostId = 1,
                    Header = "PostHeader1",
                    Body = "PostBody1"
                },
                new Post
                {
                    PostId = 2,
                    Header = "PostHeader2",
                    Body = "PostBody2"
                },
                new Post
                {
                    PostId = 3,
                    Header = "PostHeader3",
                    Body = "PostBody3"
                }
            );

            context.Comments.AddRange(
                new Comment
                {
                    CommentId = 1,
                    PostId = 1,
                    Author = "CommentAuthor1",
                    Body = "CommentBody1"
                },
                new Comment
                {
                    CommentId = 2,
                    PostId = 2,
                    Author = "CommentAuthor2",
                    Body = "CommentBody2"
                }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(RepositoryContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
