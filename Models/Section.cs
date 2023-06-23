using System.ComponentModel.DataAnnotations;

namespace KanbanProject.Models
{
    public class Section
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Assignment>? Assignments { get; set; } = new List<Assignment>();

    }
}
