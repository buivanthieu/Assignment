using System.ComponentModel.DataAnnotations;

namespace Assigment_02.DTO.Grade
{
    public class CreatGradeDTO
    {
    
        [Required]
        public string? Name {get; set;}
    }
}