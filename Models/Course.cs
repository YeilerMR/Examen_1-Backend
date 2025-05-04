using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam1_API.Models
{
    public class Course
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Schedule { get; set; }
        public required string Professor { get; set; }
        public string? ImageUrl { get; set; }
        
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
