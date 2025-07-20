using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Web.Models
{
    public class ReviewViewModel
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        
        [StringLength(1000)]
        public string? Comment { get; set; }
        
        [Required]
        public string BookId { get; set; } = string.Empty;
    }
} 