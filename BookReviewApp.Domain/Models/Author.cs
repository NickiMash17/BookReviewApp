using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Domain.Models
{
    public class Author : BaseEntity
    {
        [Key]
        public int AuthorId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;  // Add = null!
        
        [StringLength(500)]
        public string? Biography { get; set; }  // Make nullable with ?
        
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [StringLength(200)]
        public string? PhotoUrl { get; set; }  // Make nullable with ?
        
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}