using System.ComponentModel.DataAnnotations;

namespace project_demo.Model.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Roles { get; set; }
   }
}
