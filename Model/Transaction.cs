using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CargoPayAPI.Model
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid CardId { get; set; }

        [ForeignKey("CardId")]
        public Card Card { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeApplied { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
