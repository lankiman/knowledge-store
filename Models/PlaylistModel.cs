using e_learning.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_learning.Models
{
    public class PlaylistModel
    {
        [Column(TypeName = "nvarchar(50)")]
        [Key][Required] public string PlaylistId { get; set; } = Guid.NewGuid().ToString();

        [Column(TypeName = "nvarchar(100)")]
        [Required] public string PlaylistTittle { get; set; }


        [Column(TypeName = "nvarchar(2000)")]
        [Required] public string PlaylistDescription { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string? PlaylistThumbnailUrl { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public AcessType PlaylistAcessType { get; set; } = AcessType.Subscribed;

        public int PlaylistLikes { get; set; }

        public int PlaylistViews { get; set; }
    }
}
