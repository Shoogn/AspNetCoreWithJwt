using AspNetCoreWithJwt.Abstract;
using AspNetCoreWithJwt.Entities;
using AspNetCoreWithJwt.Infrastructures;
using AspNetCoreWithJwt.MessageModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreWithJwt.Services
{
    public class UserServices : IUserServices
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository,
            IOptions<AuthenticationSettings> authenticationSettingsOptions)
        {
            _authenticationSettings = authenticationSettingsOptions.Value;
            _userRepository = userRepository;
        }
        public async Task<UserResponse> GetUserAsync(UserRequest request, CancellationToken token = default)
        {
            var response = await _userRepository.GetByEmailAsync(request.Email, token);
            return new UserResponse { Email = response.Email, Name = response.Name };
        }

        public async Task<TokenResponse> SignInAsync(SignInRequest request, CancellationToken token = default)
        {
            bool respone = await _userRepository.AuthenticateAsync(request.Email, request.Password, token);
            if(respone == false)
            {
                return null;
            }
            else
            {
                return new TokenResponse { Token = GenerateEncodeJsonToken(request) };
            }
        }

        public async Task<UserResponse> SignUpAsync(SignUpRequest request, CancellationToken token = default)
        {
            var newUser = new User { Email = request.Email, UserName = request.Email, Name = request.Name };
            var result = await _userRepository.SignUpAsync(newUser, request.Password, token);

            return !result ? null : new UserResponse { Email = request.Email, Name = request.Email };
        }


        private string GenerateEncodeJsonToken(SignInRequest signInRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authenticationSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email,signInRequest.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(_authenticationSettings.ExpirationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
