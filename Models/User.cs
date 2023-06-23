using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace KanbanProject.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public List <Assignment>? Assignments { get;  } = new List<Assignment>();
    }
}
