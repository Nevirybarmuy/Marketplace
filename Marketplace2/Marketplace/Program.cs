using Microsoft.EntityFrameworkCore;
using Marketplace.Data;
using Marketplace.Models;

namespace Marketplace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddRazorPages();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                if (!db.Products.Any())
                {
                    db.Products.AddRange(
                                   new Product
                        {
                            Name = "Смартфон Galaxy S23",
                            Price = 69999.99m,
                            Quantity = 50,
                            Category = "Электроника",
                            Description = "Мощный смартфон с отличной камерой и быстрым процессором",
                            IsAvailable = true,
                            CreatedAt = DateTime.Now,
                            ImageUrl = ""
                        },
                        new Product
                        {
                            Name = "Ноутбук MacBook Air",
                            Price = 99999.99m,
                            Quantity = 25,
                            Category = "Электроника",
                            Description = "Легкий и мощный ноутбук для работы и учебы",
                            IsAvailable = true,
                            CreatedAt = DateTime.Now,
                            ImageUrl = ""
                        },
                        new Product
                        {
                            Name = "Беспроводные наушники",
                            Price = 12999.99m,
                            Quantity = 100,
                            Category = "Электроника",
                            Description = "Наушники с активным шумоподавлением",
                            IsAvailable = true,
                            CreatedAt = DateTime.Now,
                            ImageUrl = ""
                        },
                        new Product
                        {
                            Name = "Кроссовки Nike Air",
                            Price = 8999.99m,
                            Quantity = 40,
                            Category = "Одежда",
                            Description = "Удобные спортивные кроссовки для бега",
                            IsAvailable = true,
                            CreatedAt = DateTime.Now,
                            ImageUrl = ""
                        }
                    );
                    db.SaveChanges();
                    Console.WriteLine("Тестовые товары успешно добавлены в базу данных!");
                }
            }

            app.MapGet("/", () => Results.Redirect("/Products"));
            app.Run();
        }
    }
}