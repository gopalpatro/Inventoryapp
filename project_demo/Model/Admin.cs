using System.ComponentModel.DataAnnotations;

namespace project_demo.Model
{
    public class Admin
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
