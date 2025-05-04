using Exam1_API.Dtos.Student;
using Exam1_API.Models;

namespace Exam1_API.Mappers
{
    public static class StudentMapper {

        public static StudentDto ToDto(this Student studentItem){
            return new StudentDto {
                Id = studentItem.Id,
                Name = studentItem.Name,
                Email = studentItem.Email,
                Phone = studentItem.Phone,
                CourseId = studentItem.CourseId.ToString() // Assuming you want to include the CourseId as well
            };
        }

        public static Student ToStudentFromCreateDto(this CreateStudentRequestDto createUserReques) {
            return new Student {
                Name = createUserReques.Name,
                Email = createUserReques.Email,
                Phone = createUserReques.Phone,
                CourseId = createUserReques.CourseId // Assuming you want to include the CourseId as well
            };
        }


    }
}