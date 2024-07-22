using System.ComponentModel.DataAnnotations;

namespace Assigment_02.Model
{
    public class Login
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}