using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TierlistServer.Models
{
    [Table("tier_lists")]
    public class TierList
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        [StringLength(200)]
        public string Title { get; set; } = "My Tier List";

        [Required]
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }

        public User? User { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}