using Models = BlazorTechnicalExam.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorTechnicalExam.Client.Pages
{
    public partial class ImageLibrary : ComponentBase
    {
        public ImageLibrary()
        {

        }

        public Models.ImageLibrary[] ImageLibraries { get; set; }

        private ImageLibraryUpdate Modal { get; set; }

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
            Modal.ImageLibrarySelected = new Models.ImageLibrary();
            Modal.OpenModal();
        }

        private void EditImageLibrary(Models.ImageLibrary imageLibrary)
        {
            Modal.ImageLibrarySelected = imageLibrary;
            Modal.OpenModal();
        }

        public async Task RefreshList()
        {
            await InvokeAsync(GetImageLibraries);
        }
    }
}
