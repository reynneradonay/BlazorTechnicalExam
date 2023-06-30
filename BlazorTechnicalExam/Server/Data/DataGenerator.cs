using Microsoft.EntityFrameworkCore;
using BlazorTechnicalExam.Shared.Models;

namespace BlazorTechnicalExam.Server.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BlazorTechnicalExamDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BlazorTechnicalExamDbContext>>()))
            {
                if (context.ToDos.Any())
                {
                    return;
                }

                context.ToDos.AddRange(
                    new ToDo()
                    {
                        Title = "Review Blazor notes",
                        Status = ToDoStatus.Completed,
                        DueDate = DateTime.Now.AddDays(-1),
                        Category = ToDoCategory.Office
                    },
                    new ToDo()
                    {
                        Title = "Create sample Blazor app",
                        Status = ToDoStatus.Active,
                        DueDate = DateTime.Now,
                        Category = ToDoCategory.Office
                    },
                    new ToDo()
                    {
                        Title = "Laundry",
                        Status = ToDoStatus.Active,
                        DueDate = DateTime.Now.AddDays(1),
                        Category = ToDoCategory.Home
                    },
                    new ToDo()
                    {
                        Title = "Meet with friends",
                        Status = ToDoStatus.New,
                        DueDate = DateTime.Now.AddDays(7),
                        Category = ToDoCategory.Leisure
                    },
                    new ToDo()
                    {
                        Title = "Milk",
                        Status = ToDoStatus.New,
                        DueDate = DateTime.Now.AddDays(2),
                        Category = ToDoCategory.Groceries
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
