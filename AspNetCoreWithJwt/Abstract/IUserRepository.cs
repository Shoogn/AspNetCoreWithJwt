using AspNetCoreWithJwt.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreWithJwt.Abstract
{
   public interface IUserRepository
    {
        Task<bool> AuthenticateAsync(string email, string password, CancellationToken token = default);
        Task<bool> SignUpAsync(User user,  string paswword, CancellationToken token = default);
        Task<User> GetByEmailAsync(string email, CancellationToken token = default);
    }
}
