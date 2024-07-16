using System.ComponentModel.DataAnnotations;

namespace Assigment_2.Model
{
    public class Student
    {
        [Required]
        public int Id { get; set; } = new int();
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string? Major { get; set; }
    }
}