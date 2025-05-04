using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam1_API.Models
{
    public class Course
    {
        public int Id { get; set; }
        public String? Name { get; set; }
        public String? Description { get; set; }
        public String? ImageUrl { get; set; }
        public String? Schedule { get; set; }
        public String? Professor { get; set; }
    }
}