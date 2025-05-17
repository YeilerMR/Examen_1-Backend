using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam1_API.Data;
using Exam1_API.Dtos.Course;
using Exam1_API.Mappers;
using Exam1_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam1_API.Controllers
{
    [Route("Exam1_API/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly string _imagePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "UploadedImages"
        );

        public CourseController(ApplicationDBContext context)
        {
            _context = context;
            // Crear directorio si no existe
            if (!Directory.Exists(_imagePath))
            {
                Directory.CreateDirectory(_imagePath);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _context.Course.Include(c => c.Students).ToListAsync();

            var coursesDto = courses.Select(c => c.ToDto()).ToList();
            return Ok(coursesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var courseModel = await _context
                .Course.Include(c => c.Students)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (courseModel == null)
            {
                return NotFound();
            }

            var courseDto = courseModel.ToDto();
            courseDto.Students = courseModel.Students.Select(s => s.ToDto()).ToList();

            return Ok(courseDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCourseRequestDto courseDto)
        {
            if (courseDto.File == null || courseDto.File.Length == 0)
                return BadRequest("No file uploaded.");

            var courseModel = courseDto.ToCourseFromCreateDto();
            await _context.Course.AddAsync(courseModel);
            await _context.SaveChangesAsync();

            var fileName = courseModel.Id.ToString() + Path.GetExtension(courseDto.File.FileName);
            var filePath = Path.Combine(_imagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await courseDto.File.CopyToAsync(stream);
            }

            courseModel.ImageUrl = fileName;
            _context.Course.Update(courseModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = courseModel.Id },
                courseModel.ToDto()
            );
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromForm] UpdateCourseRequestDto courseDto // Cambiar de [FromBody] a [FromForm]
        )
        {
            var courseModel = await _context.Course.FirstOrDefaultAsync(_course =>
                _course.Id == id
            );
            if (courseModel == null)
            {
                return NotFound();
            }

            // Actualizar campos básicos
            courseModel.Name = courseDto.Name;
            courseModel.Description = courseDto.Description;
            courseModel.Schedule = courseDto.Schedule;
            courseModel.Professor = courseDto.Professor;

            // Manejar la imagen si se proporciona
            if (courseDto.File != null && courseDto.File.Length > 0)
            {
                // Eliminar imagen anterior si existe
                if (!string.IsNullOrEmpty(courseModel.ImageUrl))
                {
                    var oldFilePath = Path.Combine(_imagePath, courseModel.ImageUrl);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Guardar nueva imagen
                var fileName =
                    courseModel.Id.ToString() + Path.GetExtension(courseDto.File.FileName);
                var filePath = Path.Combine(_imagePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await courseDto.File.CopyToAsync(stream);
                }

                courseModel.ImageUrl = fileName;
            }

            await _context.SaveChangesAsync();

            return Ok(courseModel.ToDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var courseModel = await _context.Course.FirstOrDefaultAsync(_course =>
                _course.Id == id
            );
            if (courseModel == null)
            {
                return NotFound();
            }
            _context.Course.Remove(courseModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
