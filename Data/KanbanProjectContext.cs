using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KanbanProject.Models;

namespace KanbanProject.Data
{
    public class KanbanProjectContext : DbContext
    {
        public KanbanProjectContext (DbContextOptions<KanbanProjectContext> options)
            : base(options)
        {
        }

        public DbSet<KanbanProject.Models.Assignment> Task { get; set; } = default!;
        public DbSet<KanbanProject.Models.Tag> Tag { get; set; } = default!;
    }
}
