using Assigment_2.Interface;
using Assigment_2.Model;
using Microsoft.AspNetCore.Mvc;

namespace Assigment_02.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            _studentRepository.AddStudent(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student student)
        {
            if (id!= student.Id)
            {
                return BadRequest();
            }
            _studentRepository.UpdateStudent(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            _studentRepository.DeleteStudent(student);
            return NoContent();
        }
    }
}