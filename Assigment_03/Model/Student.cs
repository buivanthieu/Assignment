using System.ComponentModel.DataAnnotations;

namespace Assigment_02.Model
{
    public class Student
    {
        public int Id { get; set; } 
        [Required]
        public string? Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        
        public Grade? Grade { get; set; }
        public int? GradeId { get; set; } 
        public List<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}