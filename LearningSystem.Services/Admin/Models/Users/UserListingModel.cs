namespace LearningSystem.Services.Admin.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserListingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
