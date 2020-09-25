namespace LearningSystem.Services.Implementations
{
    using LearningSystem.Data;
    using LearningSystem.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly LearningSystemDbContext db;

        public UserService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<UserCoursesModel> ProfileAsync(string id)
        {
            var courses = await this.db.Users
                .Where(u => u.Id == id)
                .SelectMany(u => u.Courses.Select(c => new CourseGradeModel
                {
                    Id = c.CourseId,
                    Name = c.Course.Name,
                    Grade = c.Grade
                }))
                .ToListAsync();

            var profile = await this.db.Users
                .Where(u => u.Id == id)
                .Select(u => new UserCoursesModel
                {
                    Id = u.Id,
                    Courses = courses
                })
                .FirstOrDefaultAsync();

            return profile;
        }
    }
}
