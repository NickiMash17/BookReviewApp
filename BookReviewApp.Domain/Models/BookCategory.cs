using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    public class BookCategory : BaseEntity
    {
        [Key]
        public int BookCategoryId { get; set; }
        
        [Required]
        public int BookId { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        public Book Book { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
