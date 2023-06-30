using BlazorTechnicalExam.Server.Data;
using BlazorTechnicalExam.Server.Interfaces;
using BlazorTechnicalExam.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorTechnicalExam.Server.Services
{
    public class ToDoService : IToDoService
    {
        private readonly BlazorTechnicalExamDbContext dbContext;

        public ToDoService(BlazorTechnicalExamDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<ToDo> GetToDoList()
        {
            var query = dbContext.ToDos
                .AsQueryable();

            return query;
        }

        public ToDo GetToDoById(int toDoId)
        {
            return dbContext.ToDos
                .AsNoTracking()
                .Single(toDo => toDo.Id == toDoId);
        }

        public ToDo InsertToDo(ToDo toDo)
        {
            toDo.Status = ToDoStatus.New;
            dbContext.ToDos.Add(toDo);
            dbContext.SaveChanges();

            return toDo;
        }

        public ToDo UpdateToDo(ToDo toDo)
        {
            dbContext.ToDos.Update(toDo);
            dbContext.SaveChanges();

            return dbContext.ToDos
                .Single(i => i.Id == toDo.Id);
        }

        public void DeleteToDo(int toDoId)
        {
            var toDo = dbContext.ToDos.SingleOrDefault(toDo => toDo.Id == toDoId);
            if (toDo != null)
            {
                dbContext.ToDos.Remove(toDo);
                dbContext.SaveChanges();
            }
        }
    }
}
