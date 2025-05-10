using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    public class Review : BaseEntity
    {
        [Key]
        public int ReviewId { get; set; }
        
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        
        [StringLength(1000)]
        public string? Comment { get; set; }
        
        [Required]
        public int BookId { get; set; }
        
        public Book Book { get; set; } = null!;
        
        [Required]
        [StringLength(100)]
        public string ReviewerName { get; set; } = null!;
        
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}