using System.ComponentModel.DataAnnotations;

namespace KanbanProject.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string note {  get; set; }
        Assignment? Task { get; set; }
    }
}
