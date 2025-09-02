using ECommerceApi.Application.DTOs;

namespace ECommerceApi.Application.Interfaces
{
    public interface IProductService
    {
        Task<BaseResponse<ProductCreateDto>> Create(ProductCreateDto productCreateDto);
        Task<BaseResponse<ProductUpdateDto>> Update(Guid productId, ProductUpdateDto productUpdateDto);
        Task<BaseResponse<bool>> Delete(Guid productId);
        Task<BaseResponse<ProductResponseDto>> GetById(Guid productId);
        Task<BaseResponse<IEnumerable<ProductListDto>>> GetAll();
    }
}