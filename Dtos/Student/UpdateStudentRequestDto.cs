using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam1_API.Dtos.Student{
    public class UpdateStudentRequestDto
    {
        //public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        //public int CourseId { get; set; } // Assuming you want to include the CourseId as well
    }
}