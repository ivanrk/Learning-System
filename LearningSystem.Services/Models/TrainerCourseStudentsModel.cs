namespace LearningSystem.Services.Models
{
    using LearningSystem.Data.Models;

    public class TrainerCourseStudentsModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Grade? Grade { get; set; }
    }
}
