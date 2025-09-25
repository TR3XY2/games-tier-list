using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TierlistServer.Models
{
    [Table("games")]
    public class Game
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("rawg_id")]
        public int RawgId { get; set; }

        [Required]
        [Column("title")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column("background_image")]
        public string BackgroundImage { get; set; } = string.Empty;

        [Column("tier")]
        [StringLength(1)]
        public string? Tier { get; set; } = null;

        [Required]
        [ForeignKey("TierList")]
        [Column("tier_list_id")]
        public int TierListId { get; set; }

        public TierList? TierList { get; set; }
    }
}