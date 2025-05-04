namespace Exam1_API.Dtos.Student
{
    public class StudentDto
    {
        public int Id {get; set;}
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        //public string? CourseId { get; set; } // Assuming you want to include the CourseId as well
    }
}