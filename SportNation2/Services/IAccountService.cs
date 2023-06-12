using System;
using System.Threading.Tasks;
using SportNation2.Data.Models;
using SportNation2.Infrastructure;

namespace SportNation2.Services
{
    public interface IAccountService
    {
        Task LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
        Task SignupAsync(string email, string password, DateTime birthDate, UserGenre genre);
        Task<User> GetUserById(int userId);
        Task UpdateUserProfileAsync(string email, DateTime birthDate, UserGenre genre);
    }
}
