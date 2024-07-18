using Assigment_02.DTO;
using Assigment_02.DTO.Course;
using Assigment_02.Error;
using Assigment_02.Interface;
using Assigment_02.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using static Assigment_02.Middleware.CustomExceptionMiddleware;

namespace Assigment_02.Controller
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var courses = await _courseRepository.GetAllCourses();
                var courseDTOs = _mapper.Map<List<Course>, List<CourseDTO>>(courses);
                return Ok(courseDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            try
            {
                var course = await _courseRepository.GetCourseById(id);
                if (course == null)
                {
                    return NotFound(new ApiError { ErrorCode = 404, ErrorMessage = "Course not found" });
                }

                var courseDTO = _mapper.Map<CourseDTOIfGetById>(course);
                return Ok(courseDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                var course = await _courseRepository.GetCourseById(id);
                if (course == null)
                {
                    return NotFound(new ApiError { ErrorCode = 404, ErrorMessage = "Course not found" });
                }

                _courseRepository.DeleteCourse(course);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDTO courseDTO)
        {
            try
            {
                var course = _mapper.Map<CreateCourseDTO, Course>(courseDTO);
                _courseRepository.AddCourse(course);
                return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            try
            {
                if (id != course.Id)
                {
                    return BadRequest(new ApiError { ErrorCode = 400, ErrorMessage = "Course ID mismatch" });
                }

                var existingCourse = await _courseRepository.GetCourseById(id);
                if (existingCourse == null)
                {
                    return NotFound(new ApiError { ErrorCode = 404, ErrorMessage = "Course not found" });
                }

                _courseRepository.UpdateCourse(course);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }
    }
}
