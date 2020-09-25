namespace LearningSystem.Web.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditUserRolesViewModel
    {
        [Required]
        public string Email { get; set; }

        public IEnumerable<string> SelectedRoles { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
