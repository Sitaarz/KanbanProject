
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanProject.Models
{
    public class UserInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
