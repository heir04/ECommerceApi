using ECommerceApi.Application.DTOs;
using ECommerceApi.Application.Interfaces;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Application.Services
{
    public class UserService(ApplicationContext context) : IUserService
    {
        private readonly ApplicationContext _context = context;

        public async Task<BaseResponse<UserResponseDto>> Login(LoginDto loginDto)
        {
            var response = new BaseResponse<UserResponseDto>();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email && !u.IsDeleted);

            if (user is null)
            {
                response.Message = "Invalid email or password";
                return response;
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                response.Message = "Invalid email or password";
                return response;
            }

            response.Data = new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
            };

            response.Status = true;
            response.Message = "Login successful";
            return response;
        }

        public async Task<BaseResponse<UserCreateDto>> Register(UserCreateDto userCreateDto)
        {
            var response = new BaseResponse<UserCreateDto>();

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userCreateDto.Email && !u.IsDeleted);

            if (existingUser is not null)
            {
                response.Message = "User already exists";
                return response;
            }

            var user = new User
            {
                Email = userCreateDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password)
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Registration successful";
            return response;
        }
    }
}