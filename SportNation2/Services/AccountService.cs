using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SportNation2.Data;
using SportNation2.Data.Models;
using SportNation2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SportNation2.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor accessor;
        private readonly AppDbContext dbContext;

        public AccountService(IHttpContextAccessor accessor, AppDbContext dbContext)
        {
            this.accessor = accessor;
            this.dbContext = dbContext;
        }

        public async Task LoginAsync(string email, string password, bool rememberMe)
        {
            var user = await dbContext.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            if (!await Helpers.IsPasswordCorrect(password, user.HashedPassword))
            {
                throw new Exception("Incorrect credentials");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString("O")),
                new Claim(ClaimTypes.Gender, user.Genre.ToString())
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await accessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = rememberMe
            });
        }

        public async Task SignupAsync(string email, string password, DateTime birthDate, UserGenre genre)
        {
            if (await dbContext.Users.AnyAsync(u => u.Email == email))
            {
                throw new Exception("Email already exists");
            }

            string hashedPassword = await Helpers.HashPasswordAsync(password);

            var user = new User
            {
                Email = email,
                HashedPassword = hashedPassword,
                BirthDate = birthDate,
                Genre = (Enumerations.UserGenre)genre
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task LogoutAsync()
        {
            if (accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                await accessor.HttpContext.SignOutAsync();
            }
        }

        public async Task<User> GetUserById(int userId)
        {
            return await dbContext.Users.FindAsync(userId);
        }

        public async Task UpdateUserProfileAsync(string email, DateTime birthDate, UserGenre genre)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            user.BirthDate = birthDate;
            user.Genre = (Enumerations.UserGenre)genre;

            await dbContext.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            if (!await Helpers.IsPasswordCorrect(currentPassword, user.HashedPassword))
            {
                throw new Exception("Incorrect current password");
            }

            user.HashedPassword = await Helpers.HashPasswordAsync(newPassword);

            await dbContext.SaveChangesAsync();
        }
    }
}
