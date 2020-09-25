namespace LearningSystem.Services
{
    using LearningSystem.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingModel>> AllAsync();

        Task<CourseDetailsModel> ByIdAsync(string id);

        Task<bool> StudentIsInCourseAsync(string courseId, string studentId);

        Task<bool> SignUpAsync(string id, string studentId);

        Task<bool> SignOutAsync(string id, string studentId);
    }
}
