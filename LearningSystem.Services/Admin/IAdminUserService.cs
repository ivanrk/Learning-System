namespace LearningSystem.Services.Admin
{
    using LearningSystem.Services.Admin.Models.Users;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        Task<IEnumerable<UserListingModel>> AllAsync();
    }
}
