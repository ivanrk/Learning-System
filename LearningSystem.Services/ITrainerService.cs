namespace LearningSystem.Services
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITrainerService
    {
        Task<IEnumerable<TrainerCoursesModel>> CoursesAsync(string id);

        Task<IEnumerable<TrainerCourseStudentsModel>> StudentsInCourseAsync(string courseId);

        Task<bool> IsTrainer(string courseId, string trainerId);

        Task<bool> AddGrade(string courseId, string studentId, Grade grade);
    }
}
