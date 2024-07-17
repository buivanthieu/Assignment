using Assigment_02.DTO.Grade;
using Assigment_02.Interface;
using Assigment_02.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Assigment_02.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;
        public GradeController(IGradeRepository gradeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gradeRepository = gradeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGrade([FromBody] CreatGradeDTO gradeDTO)
        {
            var grade = _mapper.Map<CreatGradeDTO, Grade>(gradeDTO);
            _gradeRepository.AddGrade(grade);
            return Ok(grade);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {
            var grades = await _gradeRepository.GetAllGrade();
            var gradeDTOs = _mapper.Map<List<Grade>, List<GradeDTO>>(grades);
            return Ok(gradeDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGradeById(int id)
        {
            var grade = await _gradeRepository.GetGradeById(id);
            if (grade == null)
            {
                return NotFound();
            }
            var gradeDto = _mapper.Map<GradeDTOIfGetById>(grade); 
            return Ok(gradeDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGrade(int id, [FromBody] GradeDTO gradeDTO)
        {
            var grade = _gradeRepository.GetGradeById(id).Result;
            if (grade == null)
            {
                return NotFound();
            }
            var newGrade = _mapper.Map<GradeDTO, Grade>(gradeDTO);
            _gradeRepository.UpdateGrade(newGrade);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGrade(int id)
        {
            var grade = _gradeRepository.GetGradeById(id).Result;
            if (grade == null)
            {
                return NotFound();
            }
            _gradeRepository.DeleteGrade(grade);
            return NoContent();
        }
    }
}