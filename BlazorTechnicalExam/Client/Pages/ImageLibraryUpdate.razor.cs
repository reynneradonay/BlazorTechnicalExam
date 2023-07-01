using BlazorTechnicalExam.Shared.Extensions;
using BlazorTechnicalExam.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using Models = BlazorTechnicalExam.Shared.Models;

namespace BlazorTechnicalExam.Client.Pages
{
    public partial class ImageLibraryUpdate : ComponentBase
    {
        public ImageLibraryUpdate()
        {
            ImageLibrarySelected = new Models.ImageLibrary();
        }

        public Models.ImageLibrary ImageLibrarySelected { get; set; }

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

        protected async Task UpdateImageLibrary()
        {
            if (ImageLibrarySelected.Id == 0)
            {
                await Http.PostAsJsonAsync("api/imagelibrary", ImageLibrarySelected);
            }
            else
            {
                await Http.PutAsJsonAsync($"api/imagelibrary", ImageLibrarySelected);
            }

            CloseModal();
            await RefreshList();
        }

        protected async Task DeleteImageLibrary()
        {
            await Http.DeleteAsync($"api/imagelibrary/{ImageLibrarySelected.Id}");
            CloseModal();
            await RefreshList();
        }

        private async Task OnFileSelected(InputFileChangeEventArgs e)
        {
            using MemoryStream ms = new();
            await e.File.OpenReadStream().CopyToAsync(ms);
            string byteString = Convert.ToBase64String(ms.ToArray());            
            ImageLibrarySelected.MimeType = e.File.ContentType;
            ImageLibrarySelected.ImageFullPath = $"data:{ImageLibrarySelected.MimeType};base64,{byteString}";
            StateHasChanged();
        }

        public void OpenModal()
        {
            ModalTitle = ImageLibrarySelected.Id == 0 ? "Add Image" : "Update Image";
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

        protected string GetCategory(ImageLibraryCategory category)
        {
            return category.GetEnumDescription();
        }
    }
}
