using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorMegaDesk.Models;

namespace RazorMegaDesk.Data
{
    public class RazorMegaDeskContext : DbContext
    {
        public RazorMegaDeskContext (DbContextOptions<RazorMegaDeskContext> options)
            : base(options)
        {
        }

        public DbSet<RazorMegaDesk.Models.Desk> Desk { get; set; } = default!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Desk>()
                .Property(d => d.Material)
                .HasConversion(
                    v => v.ToString(),  
                    v => (DesktopMaterial)Enum.Parse(typeof(DesktopMaterial), v)  
                );
        }
    }
}
