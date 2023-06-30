using BlazorTechnicalExam.Client.Shared.Services;
using BlazorTechnicalExam.Shared.Extensions;
using BlazorTechnicalExam.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using System.Reflection.PortableExecutable;
using static System.Net.WebRequestMethods;

namespace BlazorTechnicalExam.Client.Pages
{
    public partial class ToDoUpdate : ComponentBase
    {
        public ToDoUpdate()
        {
            ToDoSelected = new ToDo();
            ToDoSelected.DueDate = DateTime.Now;
        }

        public ToDo ToDoSelected { get; set; }

        public string ModalTitle { get; set; }

        public string ModalDisplay { get; set; } = "none;";

        public string ModalClass { get; set; } = "";

        public bool ShowBackdrop { get; set; } = false;

        [Parameter]
        public EventCallback OnUpdated { get; set; }

        public async Task RefreshList()
        {
            await OnUpdated.InvokeAsync();
        }

        protected async Task UpdateToDo()
        {
            if (ToDoSelected.Id == 0)
            {
                await Http.PostAsJsonAsync("api/todo", ToDoSelected);
            }
            else
            {
                await Http.PutAsJsonAsync($"api/todo", ToDoSelected);
            }

            CloseModal();
            await RefreshList();
        }

        protected async Task DeleteToDo()
        {
            await Http.DeleteAsync($"api/todo/{ToDoSelected.Id}");
            CloseModal();
            await RefreshList();
        }

        public void OpenModal()
        {
            ModalTitle = ToDoSelected.Id == 0 ? "Add To-do" : "Update To-do";
            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            StateHasChanged();
        }

        public void CloseModal()
        {
            ModalDisplay = "none";
            ModalClass = "";
            ShowBackdrop = false;
            StateHasChanged();
        }

        protected string GetStatus(ToDoStatus status)
        {
            return status.GetEnumDescription();
        }

        protected string GetCategory(ToDoCategory category)
        {
            return category.GetEnumDescription();
        }
    }
}
