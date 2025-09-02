using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApi.Application.DTOs;
using ECommerceApi.Application.Interfaces;
using ECommerceApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserCreateDto createDto)
        {
            var response = await _userService.Register(createDto);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _userService.Login(loginDto);
            return response.Status ? Ok(response) : BadRequest(response);
        }

    }
}