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
        public int UserId { get; set; }
        
        public User User { get; set; } = null!;
        
        public DateTime ReviewDate { get; set; } = DateTime.Now;
        
        public bool IsApproved { get; set; } = true;
    }
}