using Api.Domain;
using Api.Dtos;
using Api.Infrastructure;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<CatalogContext>(op=>op.UseNpgsql());
            builder.Services.AddSingleton<IDbContextFactory, CatalogContextFactory>();

            var app = builder.Build();

            //Todo meterlo en otra bbdd
            var connectionStrings = new Dictionary<string, string>
            {
                { "User1", "Host=pgdb1:5432;Database=Catalog;Username=postgres;Password=postgres" },
                { "User2", "Host=pgdb2:5432;Database=Catalog;Username=postgres;Password=postgres" }
            };

            var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory>();
            dbContextFactory.SetConnectionString(connectionStrings);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapGet("/category/{user}", async (string user, IDbContextFactory contextFactory) =>
            {
                var dbContext = contextFactory.Create(user);
                var now = DateTime.UtcNow;

                var result = await dbContext.Set<Category>().Where(e => EF.Property<DateTime?>(e, "DeleteDate") == null ||
                                               EF.Property<DateTime?>(e, "DeleteDate") >= now).ToListAsync();
                
                return Results.Ok(result);
            })
           .WithName("GetAllCategories")
           .WithOpenApi();

            app.MapPost("/category/{user}", async (AddCategoryDto addCategory, [FromRoute] string user, IDbContextFactory contextFactory) =>
            {
                var dbContext = contextFactory.Create(user);
                var category = new Category(new Name(addCategory.name),
                                            new Description(addCategory.description),
                                            new EntityId(Guid.NewGuid()));
                dbContext.Set<Category>().Add(category);

                var now = DateTime.UtcNow;
                dbContext.Set<Category>().Entry(category).Property("CreateDate").CurrentValue = now;
                dbContext.Set<Category>().Entry(category).Property("UpdateDate").CurrentValue = now;

                await dbContext.SaveChangesAsync();

                return Results.Created($"/category/{category.Id.Value}", category.Id.Value);
            })
            .WithName("AddCategory")
            .WithOpenApi();

            app.Run();
        }       
    }
}