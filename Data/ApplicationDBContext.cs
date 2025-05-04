using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam1_API.Models;

using Microsoft.EntityFrameworkCore;

namespace Exam1_API.Data{
    public class ApplicationDBContext : DbContext {

        public ApplicationDBContext(DbContextOptions <ApplicationDBContext> options) : base(options) { }

        public DbSet<Course> Course { get; set; } 
        public DbSet<Student> Student { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId);
        }
    }
    
}