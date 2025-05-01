using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ThongTinLietSi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<LietSi> LietSis { get; set; }
        public DbSet<TinTuc> TinTucs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<NghiaTrang> NghiaTrangs { get; set; }
        public DbSet<BrowserVisit> BrowserVisits { get; set; }
        public DbSet<Role> Roles { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           

            modelBuilder.Entity<LietSi>()
               .HasOne(c => c.NghiaTrang)
               .WithMany(t => t.LietSis)
               .HasForeignKey(c => c.IDNghiaTrang)
               .OnDelete(DeleteBehavior.Restrict); // Vô hiệu hóa cascade delete

            modelBuilder.Entity<User>()
                .HasOne(c => c.Role)
                .WithMany(t => t.Users)
                .HasForeignKey(c => c.IDRole)
                .OnDelete(DeleteBehavior.Restrict); // Vô hiệu hóa cascade delete

        }


    }
}
