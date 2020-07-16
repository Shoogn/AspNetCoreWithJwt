using AspNetCoreWithJwt.Abstract;
using AspNetCoreWithJwt.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreWithJwt.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManger;

        public UserRepository(SignInManager<User> signInManager,UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManger = userManager;
        }
        public async Task<bool> AuthenticateAsync(string email, string password, CancellationToken token = default)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return result.Succeeded;
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken token = default)
        {
            var result = await _userManger.Users.FirstOrDefaultAsync(s => s.Email == email, token);
            return result;
        }

        public async Task<bool> SignUpAsync(User user, string paswword, CancellationToken token = default)
        {
            var result = await _userManger.CreateAsync(user, paswword);
            return result.Succeeded;
        }
    }
}
