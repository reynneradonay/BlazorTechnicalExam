using BlazorTechnicalExam.Shared.Extensions;
using BlazorTechnicalExam.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;
using System.Net.Http.Json;
using Models = BlazorTechnicalExam.Shared.Models;

namespace BlazorTechnicalExam.Client.Pages
{
    public partial class ImageLibraryUpdate : ComponentBase
    {
        public ImageLibraryUpdate()
        {
            ImageLibrarySelected = new Models.ImageLibrary();
            ResetErrorMessage();
        }

        public Models.ImageLibrary ImageLibrarySelected { get; set; }

        public string ModalTitle { get; set; }

        public string ModalDisplay { get; set; } = "none;";

        public string ModalClass { get; set; } = "";

        public bool ShowBackdrop { get; set; } = false;

        public bool IsFormInvalid { get; set; } = true;

        public MarkupString ErrorMessage { get; set; }

        public string ErrorMessageClass { get; set; }

        [Parameter]
        public EventCallback OnUpdated { get; set; }

        public async Task RefreshList()
        {
            await OnUpdated.InvokeAsync();
        }

        protected async Task UpdateImageLibrary()
        {
            if (string.IsNullOrWhiteSpace(ImageLibrarySelected.Title))
            {
                SetErrorMessage("alert alert-danger", $"Title is required.");
                return;
            }
            else
            {
                if (ImageLibrarySelected.Id == 0)
                {
                    await Http.PostAsJsonAsync($"api/imagelibrary", ImageLibrarySelected);
                }
                else
                {
                    await Http.PutAsJsonAsync($"api/imagelibrary", ImageLibrarySelected);
                }
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
            long maxFileSize = 1024L * 1024L * 10L; //10MB
            string[] allowedExtensions = { ".jpg", ".png" };
            IBrowserFile selectedFile = e.GetMultipleFiles()[0];
            IsFormInvalid = true;

            if (!allowedExtensions.Contains(Path.GetExtension(selectedFile.Name).ToLowerInvariant()))
            {
                SetErrorMessage("alert alert-danger", $"Invalid file type. Allowed file types are {string.Join(", ", allowedExtensions)}.");
                return;
            }

            if (selectedFile.Size > maxFileSize)
            {
                SetErrorMessage("alert alert-danger", $"File size exceeds the limit. Maximum allowed size is {maxFileSize / (1024 * 1024)} MB.");
                return;
            }

            using MemoryStream ms = new();
            await e.File.OpenReadStream(maxFileSize).CopyToAsync(ms);
            string byteString = Convert.ToBase64String(ms.ToArray());
            ImageLibrarySelected.FileName = e.File.Name;
            ImageLibrarySelected.MimeType = e.File.ContentType;
            ImageLibrarySelected.ImageFullPath = $"data:{ImageLibrarySelected.MimeType};base64,{byteString}";
            //await ms.FlushAsync();
            //await ms.DisposeAsync();

            IsFormInvalid = false;
            ClearErrorMessage();
        }

        public void OpenModal()
        {
            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            if (ImageLibrarySelected.Id == 0)
            {
                ModalTitle = "Add Image";
                ErrorMessage = new MarkupString("<strong>No file selected.</strong>");
            }
            else
            {
                ModalTitle = "Update Image";
                IsFormInvalid = false;
                ClearErrorMessage();
            }
            StateHasChanged();
        }

        public void CloseModal()
        {
            ModalDisplay = "none";
            ModalClass = "";
            ShowBackdrop = false;
            IsFormInvalid = true;
            ResetErrorMessage();
            StateHasChanged();
        }

        private void SetErrorMessage(string alertClass, string message)
        {
            ErrorMessageClass = alertClass;
            ErrorMessage = new MarkupString($"{message}");
        }

        private void ClearErrorMessage()
        {
            ErrorMessageClass = "";
            ErrorMessage = new MarkupString("");
        }

        private void ResetErrorMessage()
        {
            ErrorMessageClass = "alert alert-info";
            ErrorMessage = new MarkupString("<strong>No file selected.</strong>");
        }

        protected string GetCategory(ImageLibraryCategory category)
        {
            return category.GetEnumDescription();
        }
    }
}
