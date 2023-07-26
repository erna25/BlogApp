using Models;
using Microsoft.EntityFrameworkCore;

namespace DBRepository
{
    public class RepositoryContext : DbContext
    {
		public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
		{

		}

		public virtual DbSet<Post> Posts { get; set; }
		public virtual DbSet<Comment> Comments { get; set; }
		public virtual DbSet<Tag> Tags { get; set; }
		public virtual DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().Property(u => u.Login).IsRequired();
			modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
		}
	}
}