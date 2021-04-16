using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DATA.Entity
{
    public class dbcontext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
        }

       public DbSet<Chucvu> Chucvus { get; set; }
        public DbSet<Vitri> Vitris { get; set; }
        public DbSet<Nhanvien> Nhanviens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chucvu>().HasKey(x => x.Id);
            modelBuilder.Entity<Vitri>().HasKey(x => x.Id);
            modelBuilder.Entity<Nhanvien>().HasKey(x => x.Id);
            modelBuilder.Entity<Nhanvien>().HasOne<Vitri>(x => x.Vitri).WithMany(m => m.Nhanviens).HasForeignKey(x => x.VitriId);
            modelBuilder.Entity<Nhanvien>().HasOne<Chucvu>(x => x.Chucvu).WithMany(m => m.Nhanviens).HasForeignKey(x => x.ChucvuId);
        }
    }
}
