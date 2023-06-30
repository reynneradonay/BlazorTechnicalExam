using BlazorTechnicalExam.Shared.Extensions;
using Microsoft.JSInterop;
using System.ComponentModel;

namespace BlazorTechnicalExam.Client.Shared.Services
{
    public class Toaster
    {
        private readonly IJSRuntime _jSRuntime;

        public Toaster(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task ShowToaster(ToasterType type, string message)
        {
            await _jSRuntime.InvokeVoidAsync("ShowToastr", type.GetEnumDescription(), message);
        }

        public async Task ClearToaster()
        {
            await _jSRuntime.InvokeVoidAsync("ClearToastr");
        }
    }

    public enum ToasterType
    {
        [Description("Success")]
        Success = 1,

        [Description("Error")]
        Error,

        [Description("Info")]
        Info,

        [Description("Warning")]
        Warning
    }
}
