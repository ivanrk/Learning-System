namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CoursesController : Controller
    {
        private readonly ICourseService courses;
        private readonly ITrainerService trainers;
        private readonly UserManager<User> userManager;

        public CoursesController(ICourseService courses, ITrainerService trainers, UserManager<User> userManager)
        {
            this.courses = courses;
            this.trainers = trainers;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Details(string id)
        {
            var course = await this.courses.ByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);
                course.UserIsInCourse = await this.courses.StudentIsInCourseAsync(id, userId);
            }

            return View(course);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignUp(string id)
        {
            var userId = this.userManager.GetUserId(User);

            if (await this.trainers.IsTrainer(id, userId))
            {
                TempData["Error"] = "Trainers cannot sign up for their courses.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var success = await this.courses.SignUpAsync(id, userId);

            if (!success)
            {
                return BadRequest();
            }

            TempData["Success"] = "You have successfully enrolled for this course.";
            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignOut(string id)
        {
            var userId = this.userManager.GetUserId(User);

            var success = await this.courses.SignOutAsync(id, userId);

            if (!success)
            {
                return BadRequest();
            }

            TempData["Success"] = "You have successfully unenrolled from this course.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
