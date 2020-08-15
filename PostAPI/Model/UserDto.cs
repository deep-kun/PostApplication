using System.ComponentModel.DataAnnotations;

namespace PostAPI.Model
{
    public class UserDto
    {
        public int UserId { get; set; }
        
        public string Name { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public int Role { get; set; }
    }
}
