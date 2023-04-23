using DBRepository;
using DBRepository.Factories;
using DBRepository.Interfaces;
using DBRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using Models;
using Moq;

namespace Blog.UnitTests
{
    public class BlogRepositoryTests
    {
        [Test]
        public void AddPost_Success()
        {
            // Arrange
            var postSetMock = new Mock<DbSet<Post>>();
            var contextMock = new Mock<RepositoryContext>();

            contextMock.Setup(m => m.Posts).Returns(postSetMock.Object);

            var blogRepository = new BlogRepository(contextMock.Object);
            string postHeader = "Это заголовок тестовой записи";
            string postBody = "Это тестовая запись";
            Post post = new()
            {
                Header = postHeader,
                Body = postBody
            };

            // Act            
            blogRepository.AddPost(post);

            // Assert
            postSetMock.Verify(m => m.Add(It.IsAny<Post>()), Times.Once());
            contextMock.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void DeletePost_Success()
        {
            // Arrange
            var postSetMock = new Mock<DbSet<Post>>();
            var contextMock = new Mock<RepositoryContext>();
            contextMock.Setup(m => m.Posts).Returns(postSetMock.Object);
            var blogRepository = new BlogRepository(contextMock.Object);
            int postId = 0;

            // Act
            blogRepository.DeletePost(postId);

            // Assert
            postSetMock.Verify(m => m.Remove(It.IsAny<Post>()), Times.Once());
            contextMock.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void AddComment_Success()
        {
            // Arrange
            var commentSetMock = new Mock<DbSet<Comment>>();
            var contextMock = new Mock<RepositoryContext>();
            contextMock.Setup(m => m.Comments).Returns(commentSetMock.Object);
            var blogRepository = new BlogRepository(contextMock.Object);
            string commentAuthor = "Автор тестового комментария";
            string commentBody = "Тело тестового комментария";
            int postId = 1;
            Comment comment = new()
            {
                Author = commentAuthor,
                Body = commentBody,
                PostId = postId
            };

            // Act            
            blogRepository.AddComment(comment);

            // Assert
            commentSetMock.Verify(m => m.Add(It.IsAny<Comment>()), Times.Once());
            contextMock.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void DeleteComment_Success()
        {
            // Arrange
            var commentSetMock = new Mock<DbSet<Comment>>();
            var contextMock = new Mock<RepositoryContext>();
            contextMock.Setup(m => m.Comments).Returns(commentSetMock.Object);
            var blogRepository = new BlogRepository(contextMock.Object);
            int commentId = 1;

            // Act
            blogRepository.DeleteComment(commentId);

            // Assert
            commentSetMock.Verify(m => m.Remove(It.IsAny<Comment>()), Times.Once());
            contextMock.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}