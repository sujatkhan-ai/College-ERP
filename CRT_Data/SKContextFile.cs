using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore; 
using CRT_Entity; 

namespace CRT_Data
{
    public class SKContextFile:DbContext
    {
        public SKContextFile(DbContextOptions<SKContextFile> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Books>().ToTable("Books");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Roles>().ToTable("Roles");
            modelBuilder.Entity<AdminAccess>().ToTable("AdminAccess");
            modelBuilder.Entity<UspDisplayProc>().ToTable("UspDisplayProc");
            modelBuilder.Entity<JobPost>().ToTable("JobPost");
        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<AdminAccess> AdminAccesses { get; set; }
        public DbSet<UspDisplayProc> UspDisplayProcs { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
    }
}
