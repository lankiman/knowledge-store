using e_learning.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace e_learning.Models
{
    public class LessonModel
    {
        [Column(TypeName = "nvarchar(50)")]
        [Key]
        [Required]
        public string LessonId { get; set; } = Guid.NewGuid().ToString();

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string? LessonName { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        [Required]
        public string? LessonDescription { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public LessonCategory LessonCategory { get; set; }

        public int LessonViews { get; set; }

        public int LessonLikes { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string? LessonVideoUrl { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string? LessonThumbnailUrl { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public bool IsPublished { get; set; } = false;

        [Required]
        public AcessType LessonAcessType { get; set; } = AcessType.Subscribed;

        [ForeignKey("LessonOwner")][Required] public string? LessonOwnerId { get; set; }

        [Required] public InstructorModel? LessonOwner { get; set; }
    }
}