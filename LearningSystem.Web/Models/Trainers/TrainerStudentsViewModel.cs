namespace LearningSystem.Web.Models.Trainers
{
    using LearningSystem.Services.Models;
    using System.Collections.Generic;

    public class TrainerStudentsViewModel
    {
        public IEnumerable<TrainerCourseStudentsModel> Students { get; set; }

        public CourseListingModel Course { get; set; }
    }
}
