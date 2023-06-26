using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanProject.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; } = null!;


        [ForeignKey("Section")]
        public int SectionId { get; set; }
        public Section? Note { get; set; } = null!;


        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public Tag? Tag { get; set; } = null!;
    }
}
