using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam1_API.Data;
using Exam1_API.Dtos.Student;
using Exam1_API.Mappers;
using Exam1_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam1_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StudentController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("allStudent")]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Student.ToListAsync();
            var studentsDto = students.Select(students => students.ToDto());
            return Ok(studentsDto);
        }

        [HttpGet("getStudent/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var _student = await _context.Student.FirstOrDefaultAsync(x => x.Id == id);
            if (_student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(_student.ToDto());
        }

        [HttpPost("registerStudent")]
        public async Task<IActionResult> Create([FromBody] CreateStudentRequestDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Course.FirstOrDefaultAsync(c =>
                c.Id == studentDto.CourseId
            );
            if (course == null)
            {
                return BadRequest("Course not found");
            }

            var studentModel = new Student
            {
                Name = studentDto.Name,
                Email = studentDto.Email,
                Phone = studentDto.Phone,
                CourseId = studentDto.CourseId,
            };

            await _context.Student.AddAsync(studentModel);
            await _context.SaveChangesAsync();

            var messageBody =
                $"Estudiante: {studentModel.Name}, se ha inscrito al curso: {course.Name}";
            await FirebaseHelper.SendPushNotificationToTopicAsync(
                topic: "students_notifications",
                title: "Nuevo estudiante inscrito",
                body: messageBody
            );

            return CreatedAtAction(
                nameof(GetById),
                new { id = studentModel.Id },
                studentModel.ToDto()
            );
        }

        [HttpPut("updateStudent/{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateStudentRequestDto studentDto
        )
        {
            var studentModel = await _context.Student.FirstOrDefaultAsync(_student =>
                _student.Id == id
            );
            if (studentModel == null)
            {
                return NotFound("Student not found");
            }

            studentModel.Name = studentDto.Name;
            studentModel.Email = studentDto.Email;
            studentModel.Phone = studentDto.Phone;

            await _context.SaveChangesAsync();

            return Ok(studentModel.ToDto());
        }

        [HttpDelete("deleteStudent/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var studentModel = await _context.Student.FirstOrDefaultAsync(_student =>
                _student.Id == id
            );
            if (studentModel == null)
            {
                return NotFound("Student not found");
            }

            _context.Student.Remove(studentModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("byCourse/{courseId}")]
        public async Task<IActionResult> GetByCourseId(int courseId)
        {
            var courseExists = await _context.Course.AnyAsync(c => c.Id == courseId);
            if (!courseExists)
            {
                return NotFound("Course not found");
            }

            var students = await _context.Student.Where(s => s.CourseId == courseId).ToListAsync();

            var studentsDto = students.Select(s => s.ToDto());
            return Ok(studentsDto);
        }
    }
}
