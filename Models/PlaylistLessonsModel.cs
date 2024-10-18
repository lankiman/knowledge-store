using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_learning.Models
{
    public class PlaylistLessonsModel
    {
        [Required]
        public string PlaylistId { get; set; }

        [Required]
        public string LessonId { get; set; }

        [Required]
        public DateTime LessonAddedAt { get; set; }

        [ForeignKey(nameof(LessonId))]
        [Required] public LessonModel Lesson { get; set; }

        [ForeignKey(nameof(PlaylistId))]
        [Required] public PlaylistModel Playlist { get; set; }
    }
}
