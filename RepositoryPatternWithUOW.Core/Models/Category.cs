using System.ComponentModel.DataAnnotations;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, ErrorMessage = "Maximum 100 character")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
