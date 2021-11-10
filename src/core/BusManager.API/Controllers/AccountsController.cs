using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusManager.Application.Contracts.Account;
using BusManager.Application.Services;
using BusManager.Domain.Models;
using BusManager.Domain.Repositories;
using BusManager.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlazorProducts.Server.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userSerivce;

        public AccountsController(IUserService userSerivce)
        {
            _userSerivce = userSerivce;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _userSerivce.RegisterUser(userForRegistration);

                if (!result)
                    return BadRequest();

                return StatusCode(201);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            try
            {
                var authResponse = await _userSerivce.Login(userForAuthentication);
                return Ok(authResponse);
            }
            catch (Exception e)
            {
                return Unauthorized(new AuthResponseDto { ErrorMessage = e.Message });
            }
        }
    }
}
