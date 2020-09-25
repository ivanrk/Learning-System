namespace LearningSystem.Services.Models
{
    using System;

    public class TrainerCoursesModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Students { get; set; }
    }
}
