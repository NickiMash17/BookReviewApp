using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookReviewApp.Domain.Models
{
    public class Book : BaseEntity
    {
        [Key]
        public int BookId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [StringLength(20)]
        public string? ISBN { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        
        [StringLength(200)]
        public string? CoverImageUrl { get; set; }
        
        [Required]
        public int AuthorId { get; set; }
        
        public Author Author { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}