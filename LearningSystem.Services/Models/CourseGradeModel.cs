namespace LearningSystem.Services.Models
{
    using LearningSystem.Data.Models;

    public class CourseGradeModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Grade? Grade { get; set; }
    }
}
