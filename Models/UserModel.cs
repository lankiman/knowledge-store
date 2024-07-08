using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Models
{
    public class UserModel : IdentityUser
    {
        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string? Firstname { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string? Lastname { get; set; }

        [Column(TypeName = "nvarchar(100)")] public string? MiddleName { get; set; }
    }
}