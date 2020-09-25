namespace LearningSystem.Services.Models
{
    public class CourseDetailsModel : CourseListingModel
    {
        public string Trainer { get; set; }

        public int Students { get; set; }

        public bool UserIsInCourse { get; set; }
    }
}
