namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Admin;
    using LearningSystem.Web.Areas.Admin.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : BaseController
    {
        private readonly IAdminUserService users;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IAdminUserService users, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.users = users;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.users.AllAsync();

            foreach (var user in users)
            {
                var currentUser = await userManager.FindByEmailAsync(user.Email);
                var userRoles = await userManager.GetRolesAsync(currentUser);

                foreach (var role in userRoles)
                {
                    user.Roles.Add(role);
                }
            }

            return View(users);
        }

        public async Task<IActionResult> Edit(string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);
            var userCurrentRoles = await this.userManager.GetRolesAsync(user);

            var availableRoles = await this.roleManager.Roles.ToListAsync();
            var roles = availableRoles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                });

            var rolesList = new List<SelectListItem>();

            foreach (var item in roles)
            {
                if (userCurrentRoles.Contains(item.Text))
                {
                    item.Selected = true;
                }
                rolesList.Add(item);
            }

            return View(new EditUserRolesViewModel
            {
                Email = user.Email,
                Roles = rolesList,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string email, EditUserRolesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var user = await this.userManager.FindByEmailAsync(email);
            var userCurrentRoles = await this.userManager.GetRolesAsync(user);

            if (model.SelectedRoles == null)
            {
                if (userCurrentRoles.Any())
                {
                    await this.userManager.RemoveFromRolesAsync(user, userCurrentRoles);
                    TempData["Success"] = $"The roles of user '{user.UserName}' were successfully removed.";
                }
                return RedirectToAction(nameof(Index));
            }

            foreach (var currentRole in userCurrentRoles)
            {
                if (!model.SelectedRoles.Contains(currentRole))
                {
                    await this.userManager.RemoveFromRoleAsync(user, currentRole);
                }
            }

            foreach (var roleId in model.SelectedRoles)
            {
                var findRole = await this.roleManager.FindByIdAsync(roleId);
                var role = findRole.ToString();

                if (!await this.userManager.IsInRoleAsync(user, role))
                {
                    await this.userManager.AddToRoleAsync(user, role);
                }
            }

            TempData["Success"] = $"The roles of user '{user.UserName}' were successfully changed.";
            return RedirectToAction(nameof(Index));
        }
    }
}
