using Assigment_02.DTO.Grade;
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
    public class GradeController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;

        public GradeController(IGradeRepository gradeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gradeRepository = gradeRepository;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGrade([FromBody] CreatGradeDTO gradeDTO)
        {
            try
            {
                var grade = _mapper.Map<CreatGradeDTO, Grade>(gradeDTO);
                _gradeRepository.AddGrade(grade);
                return CreatedAtAction(nameof(GetGradeById), new { id = grade.Id }, grade);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {
            try
            {
                var grades = await _gradeRepository.GetAllGrade();
                var gradeDTOs = _mapper.Map<List<Grade>, List<GradeDTO>>(grades);
                return Ok(gradeDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGradeById(int id)
        {
            try
            {
                var grade = await _gradeRepository.GetGradeById(id);
                if (grade == null)
                {
                    return NotFound(new ApiError { ErrorCode = 404, ErrorMessage = "Grade not found" });
                }
                var gradeDto = _mapper.Map<GradeDTOIfGetById>(grade);
                return Ok(gradeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(int id, [FromBody] GradeDTO gradeDTO)
        {
            try
            {
                if (id != gradeDTO.Id)
                {
                    return BadRequest(new ApiError { ErrorCode = 400, ErrorMessage = "Grade ID mismatch" });
                }

                var grade = await _gradeRepository.GetGradeById(id);
                if (grade == null)
                {
                    return NotFound(new ApiError { ErrorCode = 404, ErrorMessage = "Grade not found" });
                }

                var newGrade = _mapper.Map<GradeDTO, Grade>(gradeDTO);
                _gradeRepository.UpdateGrade(newGrade);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            try
            {
                var grade = await _gradeRepository.GetGradeById(id);
                if (grade == null)
                {
                    return NotFound(new ApiError { ErrorCode = 404, ErrorMessage = "Grade not found" });
                }

                _gradeRepository.DeleteGrade(grade);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { ErrorCode = 500, ErrorMessage = "Internal server error" });
            }
        }
    }
}
