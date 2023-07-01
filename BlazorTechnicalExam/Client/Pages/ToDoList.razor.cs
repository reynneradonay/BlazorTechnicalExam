using BlazorTechnicalExam.Client.Components;
using BlazorTechnicalExam.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorTechnicalExam.Client.Pages
{
    public partial class ToDoList : ComponentBase
    {
        public ToDoList()
        {
            ToDo = new ToDo();
            Modal = new ToDoUpdate();
            ToDoGroupedList = new List<ToDoGrouped>();
        }

        public ToDo[] ToDos { get; set; }

        [Parameter]
        public ToDo ToDo { get; set; }

        public List<ToDoGrouped> ToDoGroupedList { get; set; }

        private ToDoUpdate Modal { get; set; }

        public ListComponent<ToDo> ToDoListComponent { get; set; }

        public string Filter { get; set; } = "";

        public string SortFilter { get; set; } = "";

        public string SortAction { get; set; } = "";

        public string GroupBy { get; set; } = null;

        private async Task GetToDos()
        {
            ToDos = await Http.GetFromJsonAsync<ToDo[]>("api/todo");
        }

        private void FillToDoGroupedList()
        {
            var groupedList = new List<ToDoGrouped>();

            if (ToDos.Any())
            {
                if (GroupBy == "Category")
                {
                    groupedList = ToDos.GroupBy(item => item.Category)
                        .Select(group => new ToDoGrouped { CategoryGroupKey = group.Key, Items = group.ToList() })
                        .ToList();
                }
                if (GroupBy == "Status")
                {
                    groupedList = ToDos.GroupBy(item => item.Status)
                        .Select(group => new ToDoGrouped { StatusGroupKey = group.Key, Items = group.ToList() })
                        .ToList();
                }
            }

            ToDoGroupedList = groupedList;
        }

        protected override async Task OnInitializedAsync()
        {
            await GetToDos();
        }

        protected async Task MarkStatus(ChangeEventArgs e, ToDo toDo)
        {
            if ((bool)e.Value)
            {
                await MarkCompleted(toDo);
            }
            else
            {
                await MarkActive(toDo);
            }

            if (!string.IsNullOrEmpty(GroupBy))
            {
                FillToDoGroupedList();
            }
        }

        protected async Task MarkCompleted(ToDo toDo)
        {
            toDo.Status = ToDoStatus.Completed;
            await Http.PutAsJsonAsync($"api/todo", toDo);
            StateHasChanged();
        }

        protected async Task MarkActive(ToDo toDo)
        {
            toDo.Status = ToDoStatus.Active;
            await Http.PutAsJsonAsync($"api/todo", toDo);
            StateHasChanged();
        }

        private void AddToDo()
        {
            Modal.ToDoSelected = new ToDo();
            Modal.OpenModal();
        }

        private void EditToDo(ToDo toDo)
        {
            Modal.ToDoSelected = toDo;
            Modal.OpenModal();
        }

        public async Task RefreshList()
        {
            //Filter = "";
            //SortFilter = "";
            //GroupBy = null;
            await InvokeAsync(GetToDos);
            await InvokeAsync(FillToDoGroupedList);
        }

        public void SetFilter(ChangeEventArgs e)
        {
            Filter = e.Value.ToString();
            GroupBy = null;
            ToDoListComponent.Refresh();
        }

        public void SetSortFilter(ChangeEventArgs e)
        {
            SortFilter = e.Value.ToString();
            GroupBy = null;
            ToDoListComponent.Refresh();
        }

        public void SetGroupBy(ChangeEventArgs e)
        {
            GroupBy = e.Value.ToString();
            Filter = "";
            SortFilter = "";
            FillToDoGroupedList();
            StateHasChanged();
        }

        public bool CheckStatus(ToDoStatus status)
        {
            if (status == ToDoStatus.Completed)
                return true;

            return false;
        }

        /// <summary>
        /// Convert value of date to elapsed days if not more than or passed 30 days, otherwise display the date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        /// TODO: Logic not yet accurate based on hours difference.
        public string CustomizeDateDisplay(DateTime date)
        {
            TimeSpan difference = DateTime.Now - date;
            int elapsedDays = difference.Days;

            if (elapsedDays > 0 && elapsedDays <= 30)
            {
                if (elapsedDays == 1)
                    return $"{elapsedDays} day ago";

                return $"{elapsedDays} days ago";
            }
            else if (elapsedDays == 0)
            {
                return "Today";
            }
            else if (elapsedDays < 0 && elapsedDays >= -30)
            {
                if (elapsedDays == -1)
                    return $"{~elapsedDays + 1} day from now";

                return $"{~elapsedDays + 1} days from now";
            }

            return date.ToLongDateString();
        }

        public string GetBadgeStyle(ToDoStatus status)
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

    public class ToDoGrouped
    {
        public ToDoGrouped() { }

        public ToDoStatus StatusGroupKey { get; set; }

        public ToDoCategory CategoryGroupKey { get; set; }

        public List<ToDo> Items { get; set; }
    }
}
