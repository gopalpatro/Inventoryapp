using System.ComponentModel.DataAnnotations;

namespace project_demo.Model
{
    public class User
    {
        [Key]
        public int USerId { get; set; }

        [Required(ErrorMessage ="username is recquired")]
        [StringLength(20,ErrorMessage ="username cannot exceed 20 char")]
        public string Username { get; set; }

        [Required(ErrorMessage = "password is recquired")]
        [StringLength(20, ErrorMessage = "pssword cannot exceed 20 char")]
        public string Password { get; set; }

        public bool IsApproved { get; set; }

        //public string Status { get; set; } = "pending";
        //public string Role { get; set; }
        //public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
