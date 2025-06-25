using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    public class Category : BaseEntity
    {
        [Key]
        public int CategoryId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        
        [StringLength(200)]
        public string? Description { get; set; }
        
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}
