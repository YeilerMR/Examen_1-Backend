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
    [Route("Exam1_API/student")]
    public class StudentController : ControllerBase
    {

        private readonly ApplicationDBContext _contex;
        public StudentController(ApplicationDBContext contex)
        {
            _contex = contex;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _contex.Students.ToListAsync();
            var studentsDto = students.Select(students => students.ToDto());
            return Ok(studentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById([FromRoute] int id)
        {
            var _student = await _contex.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (_student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(_student.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentRequestDto studentDto){
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var studentModel = studentDto.ToStudentFromCreateDto();

            await _contex.Students.AddAsync(studentModel);
            await _contex.SaveChangesAsync();

            return CreatedAtAction(nameof(getById), new { id = studentModel.Id }, studentModel.ToDto());
        }
    }
}
