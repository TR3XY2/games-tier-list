using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TierlistServer.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        [Column("user_name")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(255)]
        [Column("password_hash")]
        public string PasswordHash { get; set; } = string.Empty;

        public TierList? TierList { get; set; }
    }
}