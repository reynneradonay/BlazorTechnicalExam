using System.ComponentModel.DataAnnotations;

namespace BlazorTechnicalExam.Shared.Models
{
    public class ImageLibrary
    {
        public ImageLibrary()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "File name is required")]
        public string FileName { get; set; }

        public string Description { get; set; }

        public ImageLibraryCategory Category { get; set; }

        public string MimeType { get; set; }

        public string ImageFullPath { get; set; }
    }

    public enum ImageLibraryCategory
    {
        Portrait,
        Artsy,
        Landscape,
        Family,
        Leisure,
        Movies,
        Animals
    }
}
