﻿using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAccount accountInterface;

        public AuthenticationController(IUserAccount accountInterface)
        {
            this.accountInterface = accountInterface;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateAsync([FromBody] Register user)
        {
            if (user == null) return BadRequest("Model is empty");

            var result = await accountInterface.CreateAsync(user);
            return Ok(result);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> SignInAsync([FromBody] Login user)
        {
            if (user == null) return BadRequest("Model is empty");

            var result = await accountInterface.SignInAsync(user);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshToken token)
        {
            if (token == null) return BadRequest("Model is empty");

            var result = await accountInterface.RefreshTokenAsync(token);
            return Ok(result);
        }
    }
}
