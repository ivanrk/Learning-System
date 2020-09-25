namespace LearningSystem.Services.Models
{
    using System.Collections.Generic;

    public class UserCoursesModel
    {
        public string Id { get; set; }

        public List<CourseGradeModel> Courses { get; set; }
    }
}
