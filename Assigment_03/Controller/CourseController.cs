using Assigment_02.DTO;
using Assigment_02.DTO.Course;
using Assigment_02.Interface;
using Assigment_02.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Assigment_02.Controller
{
    [ApiController]
    [Route("api/[controller]")]

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
        public async Task<IActionResult> GetAllCourses(){
            var courses = await _courseRepository.GetAllCourses();
            var courseDTOs = _mapper.Map<List<Course>, List<CourseDTO>>(courses);
            return Ok(courseDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id){
            var course = await _courseRepository.GetCourseById(id);
            if(course == null){
                return NotFound();
            }
            var courseDTO = _mapper.Map<CourseDTOIfGetById>(course); 
                      

            return Ok(courseDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(int id){
            var course = await _courseRepository.GetCourseById(id);
            if(course == null){
                return NotFound();
            }
            _courseRepository.DeleteCourse(course);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDTO courseDTO){
            var course =  _mapper.Map<CreateCourseDTO, Course>(courseDTO);
            _courseRepository.AddCourse(course);
            return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course){
            if(id!= course.Id){
                return BadRequest();
            }
            var existingCourse = await _courseRepository.GetCourseById(id);
            if(existingCourse == null){
                return NotFound();
            }
            _courseRepository.UpdateCourse(course);
            return NoContent();
        }
    }
}