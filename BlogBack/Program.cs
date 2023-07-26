using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DBRepository.Interfaces;
using DBRepository.Repositories;
using Microsoft.AspNetCore.Http;
using DBRepository;
using AutoMapper;
using BlogBack.Services.Interfaces;
using BlogBack.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BlogBack.Helpers;
using System;
using DBRepository.Factories;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using DevExpress.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using GraphQL;
using BlogBack.BlogGraphQL.BlogSchema;
using DevExpress.XtraCharts;
using GraphQL.Server;
using System.CodeDom.Compiler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						options.RequireHttpsMetadata = false;
						options.SaveToken = true;
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidIssuer = AuthOptions.ISSUER,
							ValidAudience = AuthOptions.AUDIENCE,
							IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							ClockSkew = TimeSpan.Zero
						};
					});

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddAutoMapper();
builder.Services.AddCors();
builder.Services.AddDevExpressControls();

var options = new DbContextOptionsBuilder<RepositoryContext>().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).Options;

builder.Services.AddScoped<IRepositoryContextFactory>(provider => new RepositoryContextFactory(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBlogRepository>(provider => new BlogRepository(new RepositoryContext(options)));
builder.Services.AddScoped<IIdentityRepository>(provider => new IdentityRepository(new RepositoryContext(options)));

builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();

//builder.Services.AddDbContext<RepositoryContext>(options =>
		//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddPooledDbContextFactory<RepositoryContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services
	.AddGraphQLServer()
	.AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddProjections()
	.AddFiltering()
	.AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseRouting()
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization()
    .UseCors(
		options => {
        string frontendUrl = "https://localhost:3000";
        options.WithOrigins(frontendUrl)
                .AllowAnyMethod()
                .AllowAnyHeader();
    })
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
        //endpoints.MapControllers();
    })
    .UseMvc(routes => {
        routes.MapRoute(
            name: "default",
            template: "{controller=Blog}/{action=GetPosts}/{pageIndex?}");
    })
    .UseDevExpressControls(); 

app.MapControllers();

DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(new CustomReportStorageWebExtension(app.Environment));

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var factory = services.GetRequiredService<IRepositoryContextFactory>();

	using (var context = factory.CreateDbContext())
	{
		await DBInitializer.Initialize(context);
	}
}

app.Run();
