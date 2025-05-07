using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam1_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam1_API.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Student { get; set; }
    }
}
