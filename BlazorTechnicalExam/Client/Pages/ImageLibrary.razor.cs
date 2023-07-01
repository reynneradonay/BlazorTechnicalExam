using Models = BlazorTechnicalExam.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using BlazorTechnicalExam.Shared.Models;
using BlazorTechnicalExam.Shared.Extensions;
using System.Text.RegularExpressions;
using BlazorTechnicalExam.Client.Components;

namespace BlazorTechnicalExam.Client.Pages
{
    public partial class ImageLibrary : ComponentBase
    {
        public ImageLibrary()
        {
            Modal = new ImageLibraryUpdate();
            ImageLibraryGroupedList = new List<ImageLibraryGrouped>();
        }

        public Models.ImageLibrary[] ImageLibraries { get; set; }

        private ImageLibraryUpdate Modal { get; set; }

        public ListComponent<Models.ImageLibrary> ImageLibraryListComponent { get; set; }

        public List<ImageLibraryGrouped> ImageLibraryGroupedList { get; set; }

        public string SortAction { get; set; } = "";

        public string GroupBy { get; set; } = null;

        public string Filter { get; set; } = "";

        public bool SearchClicked { get; set; } = false;

        public int FilterCount { get; set; } = 0;

        public string SelectedGrouping { get; set; }

        private async Task GetImageLibraries()
        {
            ImageLibraries = await Http.GetFromJsonAsync<Models.ImageLibrary[]>("api/imagelibrary");
        }

        protected override async Task OnInitializedAsync()
        {
            await GetImageLibraries();
        }

        private void AddImageLibrary()
        {
            SearchClicked = false;
            Filter = "";
            ImageLibraryListComponent.SetFilter(Filter);
            Modal.ImageLibrarySelected = new Models.ImageLibrary();
            Modal.OpenModal();
        }

        private void EditImageLibrary(Models.ImageLibrary imageLibrary)
        {
            SearchClicked = false;
            Filter = "";
            ImageLibraryListComponent.SetFilter(Filter);
            Modal.ImageLibrarySelected = imageLibrary;
            Modal.OpenModal();
        }

        public async Task RefreshList()
        {
            await InvokeAsync(GetImageLibraries);
            await InvokeAsync(FillImageLibraryGroupedList);
        }

        public void Search()
        {
            SearchClicked = true;
            GroupBy = null;
            ImageLibraryListComponent.SetFilter(Filter);
            ImageLibraryListComponent.Refresh();
            FilterCount = ImageLibraryListComponent.GetFilteredItems().Count();
        }

        public void ClearSearch()
        {
            SearchClicked = false;
            Filter = "";
            GroupBy = null;
            ImageLibraryListComponent.SetFilter(Filter);
            ImageLibraryListComponent.Refresh();
        }

        public void SetGroupBy(ChangeEventArgs e)
        {
            GroupBy = e.Value.ToString();
            SearchClicked = false;
            Filter = "";

            FillImageLibraryGroupedList();

            StateHasChanged();
        }

        private void FillImageLibraryGroupedList()
        {
            var groupedList = new List<ImageLibraryGrouped>();

            if (ImageLibraries.Any())
            {
                if (GroupBy == "Category")
                {
                    groupedList = ImageLibraries.GroupBy(item => item.Category)
                        .Select(group => new ImageLibraryGrouped { CategoryGroupKey = group.Key, Items = group.ToList() })
                        .ToList();
                }
            }

            ImageLibraryGroupedList = groupedList;
        }

        protected string GetCategory(ImageLibraryCategory category)
        {
            return category.GetEnumDescription();
        }
    }

    public class ImageLibraryGrouped
    {
        public ImageLibraryGrouped() { }

        public ImageLibraryCategory CategoryGroupKey { get; set; }

        public List<Models.ImageLibrary> Items { get; set; }
    }
}
