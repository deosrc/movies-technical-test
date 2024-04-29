
using Deosrc.MoviesTechnicalTest.Api.DataAccess;
using Deosrc.MoviesTechnicalTest.Api.Services.Lookups;
using Deosrc.MoviesTechnicalTest.Api.Services.Search;
using Microsoft.EntityFrameworkCore;

namespace Deosrc.MoviesTechnicalTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<MovieDatabaseContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IMovieReadOnlyRepository>(x => x.GetRequiredService<MovieDatabaseContext>());
            builder.Services.AddScoped<ILookupService, LookupService>();
            builder.Services.AddScoped<IMovieSearchService, MovieSearchService>();

            builder.Services.AddControllers();
            builder.Services.AddProblemDetails();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseExceptionHandler();
            app.UseStatusCodePages();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
