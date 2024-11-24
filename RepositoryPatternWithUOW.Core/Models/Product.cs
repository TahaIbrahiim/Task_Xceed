using System.ComponentModel.DataAnnotations;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(200, ErrorMessage = "Product name cannot be longer than 200 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Creation date is required.")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Created by user ID is required.")]
        public int CreatedByUserId { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive number.")]
        public int Duration { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        public byte[] Image { get; set; }

        public DateTime? LastUpdatedDateTime { get; set; }

        public int? LastUpdatedByUserId { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
