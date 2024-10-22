using e_learning.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_learning.Models
{
    public class TemporaryLessonModel
    {
        [Key]
        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public DateTime UpdatedAt { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string TempLessonUrl { get; set; }

        [Required]
        public string LessonOwnerId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public LessonVideoStatus LessonVideoStatus { get; set; } = LessonVideoStatus.Draft;


        [ForeignKey(nameof(LessonOwnerId))]
        [Required] public InstructorModel instructor { get; set; }

        public TemporaryLessonDetailsModel? TemporaryLessonDetails { get; set; }
    }
}
