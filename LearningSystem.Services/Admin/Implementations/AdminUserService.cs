namespace LearningSystem.Services.Admin.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LearningSystem.Data;
    using LearningSystem.Services.Admin.Models.Users;
    using Microsoft.EntityFrameworkCore;

    public class AdminUserService : IAdminUserService
    {
        private readonly LearningSystemDbContext db;

        public AdminUserService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<UserListingModel>> AllAsync()
        {
            return await this.db
                .Users
                .Select(u => new UserListingModel
                {
                    Name = u.Name,
                    Email = u.Email
                })
                .ToListAsync();
        }
    }
}
