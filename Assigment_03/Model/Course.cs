using System.ComponentModel.DataAnnotations;

namespace Assigment_02.Model
{
    public class Course
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }

        public List<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}