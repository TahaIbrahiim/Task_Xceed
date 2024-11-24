using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, ErrorMessage = "Username cannot be longer than 100 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression(@"^(Admin|Client)$", ErrorMessage = "Role must be either 'Admin' or 'Client'.")]
        public string Role { get; set; } // "Admin" or "Client"
    }
}
