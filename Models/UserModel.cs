using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<IdentityUserClaim<int>>? Claims { get; set; }


        [Column(TypeName = "nvarchar(100)")] public string? MiddleName { get; set; }
    }
}