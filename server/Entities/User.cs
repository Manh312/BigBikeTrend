using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Address { get; set; } = "";
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpire { get; set; }


    }
}
