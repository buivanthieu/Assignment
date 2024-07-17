using Assigment_02.DTO;
using Assigment_02.DTO.Student;
using Assigment_02.Interface;
using Assigment_02.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Assigment_02.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IGradeRepository _gradeRepository;
        public StudentController(IStudentRepository studentRepository, IMapper mapper, IGradeRepository gradeRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _gradeRepository = gradeRepository;

        }

        [HttpPost("gradeId")]
        public async Task<IActionResult> CreateStudent(int gradeId,[FromBody] CreateStudentDTO createStudentDTO)
        {
            var student = _mapper.Map<Student>(createStudentDTO);
            student.GradeId = gradeId;
            await _studentRepository.AddStudent(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetStudentById(int id){
            var student = await _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            var studentDto = _mapper.Map<StudentDTOIfGetById>(student);
            return Ok(studentDto);
        }
        [HttpGet]
        public async Task<ActionResult<List<StudentDTO>>> GetAllStudents(){
            var students = await _studentRepository.GetAllStudents();
            var studentDtos = _mapper.Map<List<Student>, List<StudentDTO>>(students);
            return studentDtos;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id){
            var student = await _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            await _studentRepository.DeleteStudent(student);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDTO studentDTO){
            var student = await _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            var newStudent = _mapper.Map<StudentDTO, Student>(studentDTO);
            await _studentRepository.UpdateStudent(newStudent);
            return NoContent();
        }


        [HttpPost("students/{studentId}/courses/{courseId}")]
        public async Task<IActionResult> RegisterCourse(int studentId, int courseId)
        {
            try
            {
                await _studentRepository.AddCourseToStudent(studentId, courseId);
                return Ok(new { Message = "Course registered successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}