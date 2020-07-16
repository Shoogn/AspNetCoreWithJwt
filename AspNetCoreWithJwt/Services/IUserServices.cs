using AspNetCoreWithJwt.MessageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreWithJwt.Services
{
   public interface IUserServices
    {
        Task<UserResponse> GetUserAsync(UserRequest request, CancellationToken token = default);
        Task<UserResponse> SignUpAsync(SignUpRequest request, CancellationToken token = default);
        Task<TokenResponse> SignInAsync(SignInRequest request, CancellationToken token = default);
    }
}
