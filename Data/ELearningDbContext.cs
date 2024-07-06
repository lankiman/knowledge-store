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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().HasIndex(u => u.PhoneNumber).IsUnique();

            modelBuilder.Entity<InstructorModel>().Property(i => i.Rating).HasPrecision(3, 2);

            // // Configure the one-to-many relationship for lesson ownership
            // modelBuilder.Entity<LessonModel>()
            //     .HasOne(l => l.LessonOwner)
            //     .WithMany(u => u.UserOwnedLessons)
            //     .HasForeignKey(l => l.LessonOwnerId)
            //     .OnDelete(DeleteBehavior.Restrict);
            //
            // // Configure the composite key for UserPaidLessonsModel
            // modelBuilder.Entity<UserPaidLessonsModel>()
            //     .HasKey(ul => new { ul.UserId, ul.LessonId });
            //
            // // Configure many-to-many relationship between User and Lesson
            // modelBuilder.Entity<UserPaidLessonsModel>()
            //     .HasOne(userPaidLesson => userPaidLesson.User)
            //     .WithMany(user => user.UserPaidLessons)
            //     .HasForeignKey(userPaidLesson => userPaidLesson.UserId);
            //
            // modelBuilder.Entity<UserPaidLessonsModel>()
            //     .HasOne(userPaidLesson => userPaidLesson.Lesson)
            //     .WithMany(lessons => lessons.UserPaidLessons)
            //     .HasForeignKey(userPaidLesson => userPaidLesson.LessonId);
            //
            // // Indexing to improve performance
            // modelBuilder.Entity<UserPaidLessonsModel>()
            //     .HasIndex(userPaidLesson => userPaidLesson.UserId);
            //
            // modelBuilder.Entity<UserPaidLessonsModel>()
            //     .HasIndex(userPaidLesson => userPaidLesson.LessonId);
        }
    }
}