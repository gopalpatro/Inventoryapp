using System.ComponentModel.DataAnnotations;

namespace project_demo.Model
{
    public class Registrationrequest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "username is recquired")]
        [StringLength(20, ErrorMessage = "username cannot exceed 20 char")]
        public string Username { get; set; }

        [Required(ErrorMessage = "password is recquired")]
        [StringLength(20, ErrorMessage = "pssword cannot exceed 20 char")]
        public string Password { get; set; }
        public string Role { get; set; }

        public bool IsApproved { get; set; }
       // public bool IsProcessed { get; set; }
    }
}
