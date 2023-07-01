using BlazorTechnicalExam.Server.Data;
using BlazorTechnicalExam.Server.Interfaces;
using BlazorTechnicalExam.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorTechnicalExam.Server.Services
{
    public class ImageLibraryService : IImageLibraryService
    {
        private readonly BlazorTechnicalExamDbContext dbContext;

        public ImageLibraryService(BlazorTechnicalExamDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void DeleteImageLibrary(int imageLibraryId)
        {
            var imageLibrary = dbContext.ImageLibraries.SingleOrDefault(imageLibrary => imageLibrary.Id == imageLibraryId);
            if (imageLibrary != null)
            {
                dbContext.ImageLibraries.Remove(imageLibrary);
                dbContext.SaveChanges();
            }
        }

        public ImageLibrary GetImageLibraryById(int imageLibraryId)
        {
            return dbContext.ImageLibraries
                .AsNoTracking()
                .Single(imageLibrary => imageLibrary.Id == imageLibraryId);
        }

        public IQueryable<ImageLibrary> GetImageLibraryList()
        {
            var query = dbContext.ImageLibraries
                .AsQueryable();

            return query;
        }

        public ImageLibrary InsertImageLibrary(ImageLibrary imageLibrary)
        {
            dbContext.ImageLibraries.Add(imageLibrary);
            dbContext.SaveChanges();

            return imageLibrary;
        }

        public ImageLibrary UpdateImageLibrary(ImageLibrary imageLibrary)
        {
            dbContext.ImageLibraries.Update(imageLibrary);
            dbContext.SaveChanges();

            return dbContext.ImageLibraries
                .Single(i => i.Id == imageLibrary.Id);
        }
    }
}
