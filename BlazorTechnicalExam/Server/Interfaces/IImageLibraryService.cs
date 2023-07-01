using BlazorTechnicalExam.Shared.Models;

namespace BlazorTechnicalExam.Server.Interfaces
{
    public interface IImageLibraryService
    {
        IQueryable<ImageLibrary> GetImageLibraryList();

        ImageLibrary GetImageLibraryById(int imageLibraryId);

        ImageLibrary InsertImageLibrary(ImageLibrary imageLibrary);

        ImageLibrary UpdateImageLibrary(ImageLibrary imageLibrary);

        void DeleteImageLibrary(int imageLibraryId);
    }
}
