using Coffee.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<WaitlistEntry> Waitlist { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Event>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Ticket>()
                .HasOne(t => t.Event)
                .WithMany(e => e.Tickets)
                .HasForeignKey(t => t.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WaitlistEntry>()
                .HasOne(w => w.Event)
                .WithMany(e => e.WaitlistEntries)
                .HasForeignKey(w => w.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Event>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Lecturer>().HasQueryFilter(l => !l.IsDeleted);
        }
    }
}
