using BlazorTechnicalExam.Client.Shared.Services;
using BlazorTechnicalExam.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Reflection.PortableExecutable;

namespace BlazorTechnicalExam.Client.Pages
{
    public partial class ToDoList : ComponentBase
    {
        public ToDoList()
        {
            ToDo = new ToDo();
            IsEditing = false;
        }

        public ToDo[] ToDos { get; set; }

        [Parameter]
        public ToDo ToDo { get; set; }

        [Parameter]
        public ToDo ToDoSelected { get; set; }

        private bool IsEditing { get; set; }

        private async Task GetToDos()
        {
            ToDos = await Http.GetFromJsonAsync<ToDo[]>("api/todo");
        }

        protected override async Task OnInitializedAsync()
        {
            await GetToDos();
        }

        protected async Task AddToDo()
        {
            var response = await Http.PostAsJsonAsync("api/todo", ToDo);
            await response.Content.ReadFromJsonAsync<ToDo>();
            await Toaster.ShowToaster(ToasterType.Success, "Task added.");
            await GetToDos();
            StateHasChanged();
        }

        protected async Task UpdateToDo()
        {
            await Http.PutAsJsonAsync($"api/todo", ToDoSelected);
            await Toaster.ShowToaster(ToasterType.Success, "Task updated.");
            StateHasChanged();
        }

        protected async Task DeleteToDo(int id)
        {
            await Http.DeleteAsync($"api/todo/{id}");
            await Toaster.ShowToaster(ToasterType.Error, "Task deleted.");
            await GetToDos();
            StateHasChanged();
        }

        private void EditToDo()
        {
            IsEditing = true;
        }

        private string GetBadgeStyle(ToDoStatus status)
        {
            string style = status switch
            {
                ToDoStatus.New => "bg-secondary",
                ToDoStatus.Active => "bg-primary",
                ToDoStatus.Completed => "bg-success",
                ToDoStatus.Postponed => "bg-danger",
                _ => "bg-secondary",
            };
            return style;
        }
    }
}
