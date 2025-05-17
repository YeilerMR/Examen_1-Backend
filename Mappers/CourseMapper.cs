using Exam1_API.Dtos.Course;
using Exam1_API.Dtos.Student;
using Exam1_API.Models;

namespace Exam1_API.Mappers
{
    public static class CourseMapper
    {
        public static CourseDto ToDto(this Course courseItem)
        {
            return new CourseDto
            {
                Id = courseItem.Id,
                Name = courseItem.Name,
                Description = courseItem.Description,
                ImageUrl = courseItem.ImageUrl,
                Schedule = courseItem.Schedule,
                Professor = courseItem.Professor,
                Students =
                    courseItem.Students?.Select(s => s.ToDto()).ToList() ?? new List<StudentDto>(),
            };
        }

        public static Course ToCourseFromCreateDto(this CreateCourseRequestDto createUserRequest)
        {
            return new Course
            {
                Name = createUserRequest.Name,
                Description = createUserRequest.Description,
                Schedule = createUserRequest.Schedule,
                Professor = createUserRequest.Professor,
            };
        }
    }
}
