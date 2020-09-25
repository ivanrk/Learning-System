namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TrainerService : ITrainerService
    {
        private readonly LearningSystemDbContext db;

        public TrainerService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<TrainerCoursesModel>> CoursesAsync(string id)
            => await this.db
                .Courses
                .Where(c => c.TrainerId == id)
                .Select(c => new TrainerCoursesModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Students = c.Students.Count
                })
                .ToListAsync();

        public async Task<IEnumerable<TrainerCourseStudentsModel>> StudentsInCourseAsync(string courseId)
            => await this.db
                .Courses
                .Where(c => c.Id == courseId)
                .SelectMany(c => c.Students.Select(s => s.Student))
                .Select(s => new TrainerCourseStudentsModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Grade = s.Courses.Where(c => c.CourseId == courseId).Select(st => st.Grade).FirstOrDefault()
                })
                .ToListAsync();

        public async Task<bool> IsTrainer(string courseId, string trainerId)
            => await this.db
                .Courses
                .AnyAsync(c => c.Id == courseId && c.TrainerId == trainerId);

        public async Task<bool> AddGrade(string courseId, string studentId, Grade grade)
        {
            var studentInCourse = await this.db.FindAsync<StudentCourse>(courseId, studentId);

            if (studentInCourse == null)
            {
                return false;
            }

            studentInCourse.Grade = grade;

            await this.db.SaveChangesAsync();
            return true;
        }
    }
}
