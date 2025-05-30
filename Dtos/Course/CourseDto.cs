using Exam1_API.Dtos.Student;

namespace Exam1_API.Dtos.Course
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Schedule { get; set; }
        public string Professor { get; set; }
        public List<StudentDto> Students { get; set; }
    }
}
