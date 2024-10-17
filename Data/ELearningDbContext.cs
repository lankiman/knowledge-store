using e_learning.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Data
{
    public class ELearningDbContext(DbContextOptions<ELearningDbContext> options)
        : IdentityDbContext<UserModel>(options)
    {
        public DbSet<LessonModel>? Lessons { get; set; }

        public DbSet<InstructorModel>? Instructors { get; set; }

        public DbSet<PlaylistModel>? Playlists { get; set; }

        public DbSet<PlaylistLessonsModel>? PlaylistsLessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlaylistLessonsModel>().HasKey(pl => new { pl.PlaylistId, pl.LessonId });

            modelBuilder.Entity<UserModel>().HasIndex(u => u.PhoneNumber).IsUnique();

            modelBuilder.Entity<InstructorModel>().Property(i => i.Rating).HasPrecision(3, 2);
        }
    }
}