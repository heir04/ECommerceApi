using ECommerceApi.Application.DTOs;

namespace ECommerceApi.Application.Interfaces
{
    public interface IOrderService
    {
        Task<BaseResponse<OrderCreateDto>> Create(OrderCreateDto orderCreateDto, Guid ProductId);
        Task<BaseResponse<bool>> Delete(Guid orderId);
        Task<BaseResponse<OrderResponseDto>> GetById(Guid orderId);
        Task<BaseResponse<IEnumerable<OrderListDto>>> GetAll();
        Task<BaseResponse<OrderResponseDto>> GetUserActiveCart(Guid userId);
    }
}