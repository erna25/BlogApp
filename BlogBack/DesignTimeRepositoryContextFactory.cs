﻿using DBRepository;
using DBRepository.Factories;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BlogBack
{
    public class DesignTimeRepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
	{
		public RepositoryContext CreateDbContext(string[] args)
		{
			var builder = new ConfigurationBuilder()
				 .SetBasePath(Directory.GetCurrentDirectory())
				 .AddJsonFile("appsettings.json");

			var config = builder.Build();
			var connectionString = config.GetConnectionString("DefaultConnection");
			var repositoryFactory = new RepositoryContextFactory(connectionString);

			return repositoryFactory.CreateDbContext();
		}
	}
}
