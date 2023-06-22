using System.ComponentModel.DataAnnotations;

namespace KanbanProject.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Colour { get; set; }
        public string? Title { get; set; }
        List <Assignment>? Assignments { get; set; }
    }
}
