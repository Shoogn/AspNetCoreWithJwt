using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreWithJwt.MessageModels;
using AspNetCoreWithJwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWithJwt.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
   // [ApiExplorerSettings(IgnoreApi = true)]
    public class MembershipValueController : ControllerBase
    {
        // MembershipValue
        private readonly IUserServices _userServices;
        public MembershipValueController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            if (claim == null) return Unauthorized();
            var token = await _userServices.GetUserAsync(new MessageModels.UserRequest
            {
                Email = claim.Value
            });

            return Ok(token);
        }
        
        [AllowAnonymous]
        [HttpPost("logindetails")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var token = await _userServices.SignInAsync(request);
            if (token == null) return BadRequest();
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            var user = await _userServices.SignUpAsync(request);
            if (user == null)
                return BadRequest();
            return Ok(CreatedAtAction(nameof(Get), new { }, null));
            
        }
    }
}