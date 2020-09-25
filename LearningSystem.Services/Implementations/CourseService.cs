namespace LearningSystem.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Models;
    using Microsoft.EntityFrameworkCore;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CourseListingModel>> AllAsync()
            => await this.db
                .Courses
                .OrderBy(c => c.StartDate)
                .Select(c => new CourseListingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate
                })
                .ToListAsync();

        public async Task<CourseDetailsModel> ByIdAsync(string id)
            => await this.db
                .Courses
                .Where(c => c.Id == id)
                .Select(c => new CourseDetailsModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Students = c.Students.Count,
                    Trainer = c.Trainer.Name
                })
                .FirstOrDefaultAsync();

        public async Task<bool> StudentIsInCourseAsync(string courseId, string studentId)
            => await this.db.Courses
                .AnyAsync(c => c.Id == courseId && c.Students.Any(s => s.StudentId == studentId));

        public async Task<bool> SignUpAsync(string id, string studentId)
        {
            var courseInfo = await GetCourseInfo(id, studentId);

            if (courseInfo == null || courseInfo.StartDate < DateTime.UtcNow || courseInfo.UserIsInCourse)
            {
                return false;
            }

            var studentCourse = new StudentCourse
            {
                CourseId = id,
                StudentId = studentId
            };

            this.db.Add(studentCourse);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SignOutAsync(string id, string studentId)
        {
            var courseInfo = await this.GetCourseInfo(id, studentId);

            if (courseInfo == null || courseInfo.StartDate < DateTime.UtcNow || !courseInfo.UserIsInCourse)
            {
                return false;
            }

            var studentCourse = new StudentCourse
            {
                CourseId = id,
                StudentId = studentId
            };

            this.db.Remove(studentCourse);
            await this.db.SaveChangesAsync();

            return true;
        }

        private async Task<CourseBasicModel> GetCourseInfo(string id, string studentId)
            => await this.db.Courses
                .Where(c => c.Id == id)
                .Select(c => new CourseBasicModel
                {
                    StartDate = c.StartDate,
                    UserIsInCourse = c.Students.Any(s => s.StudentId == studentId)
                })
                .FirstOrDefaultAsync();
    }
}
