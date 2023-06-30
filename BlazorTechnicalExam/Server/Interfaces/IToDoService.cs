using BlazorTechnicalExam.Shared.Models;

namespace BlazorTechnicalExam.Server.Interfaces
{
    public interface IToDoService
    {
        IQueryable<ToDo> GetToDoList();

        ToDo GetToDoById(int toDoId);

        ToDo InsertToDo(ToDo toDo);

        ToDo UpdateToDo(ToDo toDo);

        void DeleteToDo(int toDoId);
    }
}
