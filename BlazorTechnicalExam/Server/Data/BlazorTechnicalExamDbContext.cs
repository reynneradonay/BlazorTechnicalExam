using BlazorTechnicalExam.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorTechnicalExam.Server.Data
{
    public class BlazorTechnicalExamDbContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }

        public DbSet<ImageLibrary> ImageLibraries { get; set; }

        public BlazorTechnicalExamDbContext(DbContextOptions<BlazorTechnicalExamDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>(toDo => { toDo.Property(p => p.Id).ValueGeneratedOnAdd(); });
            modelBuilder.Entity<ImageLibrary>(imageLibrary => { imageLibrary.Property(p => p.Id).ValueGeneratedOnAdd(); });
            base.OnModelCreating(modelBuilder);
        }
    }
}
