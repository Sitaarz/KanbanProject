using System.ComponentModel.DataAnnotations;

namespace KanbanProject.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public User? User { get; set; }
        public Section? Note { get; set; }
        public Tag? Tag { get; set; }
    }
}
