using System.ComponentModel.DataAnnotations;

namespace BlazorTechnicalExam.Shared.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        public ToDoStatus Status { get; set; }

        public DateTime DueDate { get; set; }

        public ToDoCategory Category{ get; set; }
    }

    public enum ToDoStatus
    {
        New,
        Active,
        Completed,
        Postponed
    }

    public enum ToDoCategory
    {
        Home,
        Office,
        Groceries,
        Leisure
    }
}
