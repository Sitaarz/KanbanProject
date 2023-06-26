using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KanbanProject.Models;
using System.Reflection.Metadata;

namespace KanbanProject.Data
{
    public class KanbanProjectContext : DbContext
    {
        public KanbanProjectContext(DbContextOptions<KanbanProjectContext> options)
            : base(options)
        {
        }

        public DbSet<KanbanProject.Models.Section> Section { get; set; } = default!;
        public DbSet<KanbanProject.Models.User> User { get; set; } = default!;
        public DbSet<KanbanProject.Models.Tag> Tag { get; set; } = default!;
        public DbSet<KanbanProject.Models.Assignment> Assignment { get; set; } = default!;
        public DbSet<KanbanProject.Models.UserInformation> UserInformation { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.User)
                .WithMany(u => u.Assignments)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Note)
                .WithMany(s => s.Assignments)
                .HasForeignKey(a => a.SectionId);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Tag)
                .WithMany(t => t.Assignments)
                .HasForeignKey(a => a.TagId);
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserInformation)
                .WithOne(ui => ui.User)
                .HasForeignKey<UserInformation>(ui => ui.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
