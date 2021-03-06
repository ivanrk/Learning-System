﻿namespace LearningSystem.Services
{
    using LearningSystem.Services.Models;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<UserCoursesModel> ProfileAsync(string id);
    }
}
