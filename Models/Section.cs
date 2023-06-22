﻿using System.ComponentModel.DataAnnotations;

namespace KanbanProject.Models
{
    public class Section
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        List<Assignment>? assignments { get; set; }

    }
}
