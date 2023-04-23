using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DBRepository
{
    public static class DBInitializer
    {
		public async static Task Initialize(RepositoryContext context)
		{
			await context.Database.MigrateAsync();

			var userCount = await context.Users.CountAsync().ConfigureAwait(false);
			if (userCount == 0)
			{
				context.Users.Add(new Models.User()
				{
					Login = "admin",
					Password = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=",
					isAdmin = true
				});
				context.Users.Add(new Models.User()
				{
					Login = "erna",
					Password = "BPiZbadjt6lpsQKO4wB1aerzpjVIbdqyEdUSyFud+Ps=",
					isAdmin = false
				});
				context.Posts.Add(new Models.Post()
				{
					Header = "Первая запись в блоге",
					Body = "Учетные данные админа: admin/admin, " +
							"Учетные данные пользователя: erna/user",
					CreatedDate = DateTime.Now
				});

				await context.SaveChangesAsync().ConfigureAwait(false);
			}
		}
	}
}
