using ECommerceApi.Application.DTOs;

namespace ECommerceApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserCreateDto>> Register(UserCreateDto userCreateDto);
        Task<BaseResponse<UserResponseDto>> Login(LoginDto loginDto);
    }
}