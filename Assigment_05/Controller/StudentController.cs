using Assigment_02.DTO;
using Assigment_02.DTO.Student;
using Assigment_02.Interface;
using Assigment_02.Model;
using Assigment_02.Error;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Assigment_02.Controller
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost("{gradeId}")]
        public async Task<IActionResult> CreateStudent(int gradeId, [FromBody] CreateStudentDTO createStudentDTO)
        {
            try
            {
                var student = _mapper.Map<Student>(createStudentDTO);
                student.GradeId = gradeId;
                await _studentRepository.AddStudent(student);
                return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await _studentRepository.GetStudentById(id);
                if (student == null)
                {
                    return NotFound(new ApiError { ErrorCode = 404, ErrorMessage = "Student not found" });
                }
                var studentDto = _mapper.Map<StudentDTOIfGetById>(student);
                return Ok(studentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentDTO>>> GetAllStudents()
        {
            try
            {
                var students = await _studentRepository.GetAllStudents();
                var studentDtos = _mapper.Map<List<Student>, List<StudentDTO>>(students);
                return Ok(studentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _studentRepository.GetStudentById(id);
                if (student == null)
                {
                    return NotFound(new ApiError { ErrorCode = 404, ErrorMessage = "Student not found" });
                }
                await _studentRepository.DeleteStudent(student);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDTO studentDTO)
        {
            try
            {
                

                var student = await _studentRepository.GetStudentById(id);
                if (student == null)
                {
                    return NotFound(new ApiError { ErrorCode = 404, ErrorMessage = "Student not found" });
                }

                var newStudent = _mapper.Map<StudentDTO, Student>(studentDTO);
                await _studentRepository.UpdateStudent(newStudent);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{studentId}/courses/{courseId}")]
        public async Task<IActionResult> RegisterCourse(int studentId, int courseId)
        {
            try
            {
                await _studentRepository.AddCourseToStudent(studentId, courseId);
                return Ok(new { Message = "Course registered successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiError { ErrorCode = 400, ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }
    }
}
