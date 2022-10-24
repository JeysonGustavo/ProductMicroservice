using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Product.API.Infrastructure.Data
{
    public static class InitializeDb
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }

        private static void SeedData(AppDbContext context)
        {
            context.Database.Migrate();
            // if (!context.Categories.Any())
            // {
            //     context.Categories.Add(new CategoryModel() { Id = 1, CategoryName = "Microsoft" });
            //     context.Categories.Add(new CategoryModel() { Id = 2, CategoryName = "Docker" });
            //     context.Categories.Add(new CategoryModel() { Id = 3, CategoryName = "Linux" });

            //     context.SaveChanges();
            // }

            // if (!context.Products.Any())
            // {
            //     context.Products.Add(new ProductModel() { Id = 1, CategoryId = 1, ProductName = "DevOps", Cost = 100.99M });
            //     context.Products.Add(new ProductModel() { Id = 2, CategoryId = 1, ProductName = "Office 365", Cost = 209.99M });
            //     context.Products.Add(new ProductModel() { Id = 3, CategoryId = 2, ProductName = "Docker Hub", Cost = 0M });
            //     context.Products.Add(new ProductModel() { Id = 4, CategoryId = 3, ProductName = "Ubuntu", Cost = 0M });

            //     context.SaveChanges();
            // }
        }
    }
}