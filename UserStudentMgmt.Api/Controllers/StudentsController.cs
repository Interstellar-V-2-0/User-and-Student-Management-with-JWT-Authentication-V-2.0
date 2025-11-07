using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserStudentMgmt.Application.DTOs.Students;
using UserStudentMgmt.Application.Interfaces;
using UserStudentMgmt.Domain.Entities;

namespace UserStudentMgmt.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }
    
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAllAsync() => Ok(await _studentService.GetAllAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromBody] StudentRequestDto dto)
        {
            var created = await _studentService.CreateAsync(dto);
            return Created($"/api/students/{created.Id}", created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] StudentRequestDto dto)
        {
            var exists = await _studentService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _studentService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var exists = await _studentService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
