using e_learning.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_learning.Models
{
    public class TemporaryLessonDetailsModel
    {
        [Column(TypeName = "nvarchar(50)")]
        [Key]
        [Required]
        public string TemporaryLessonDetialsId { get; set; } = Guid.NewGuid().ToString();

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string? TemporaryLessonName { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        [Required]
        public string? TemporaryLessonDescription { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public LessonCategory TemporaryLessonCategory { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        [Required]
        public string? TemporaryLessonThumbnailUrl { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public AcessType TemporaryLessonAcessType { get; set; } = AcessType.Subscribed;


        [Required] public string TemporaryLessonId { get; set; }


        [ForeignKey(nameof(TemporaryLessonId))]
        [Required] public TemporaryLessonModel TemporaryLesson { get; set; }

    }
}
