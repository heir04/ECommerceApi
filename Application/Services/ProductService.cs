

using ECommerceApi.Application.DTOs;
using ECommerceApi.Application.Interfaces;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Application.Services
{
    public class ProductService(ApplicationContext context) : IProductService
    {
        private readonly ApplicationContext _context = context;
        public async Task<BaseResponse<ProductCreateDto>> Create(ProductCreateDto createDto)
        {
            var response = new BaseResponse<ProductCreateDto>();

            var product = new Product
            {
                Name = createDto.Name,
                Description = createDto.Description,
                Price = createDto.Price,
                StockQuantity = createDto.StockQuantity
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            response.Data = createDto;
            response.Status = true;
            response.Message = "Success";
            return response;
        }

        public async Task<BaseResponse<bool>> Delete(Guid productId)
        {
            var response = new BaseResponse<bool>();

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);
            if (product == null)
            {
                response.Status = false;
                response.Message = "Product not found";
                return response;
            }

            product.IsDeleted = true;
            await _context.SaveChangesAsync();

            response.Data = true;
            response.Status = true;
            response.Message = "Success";
            return response;
        }

        public async Task<BaseResponse<IEnumerable<ProductListDto>>> GetAll()
        {
            var response = new BaseResponse<IEnumerable<ProductListDto>>();

            var products = await _context.Products
                .Where(p => !p.IsDeleted)
                .Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();

            response.Data = products;
            response.Status = true;
            response.Message = "Success";
            return response;
        }

        public async Task<BaseResponse<ProductResponseDto>> GetById(Guid productId)
        {
            var response = new BaseResponse<ProductResponseDto>();

            var product = await _context.Products
                .Where(p => p.Id == productId && !p.IsDeleted)
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                response.Status = false;
                response.Message = "Product not found";
                return response;
            }

            response.Data = product;
            response.Status = true;
            response.Message = "Success";
            return response;
        }

        public async Task<BaseResponse<ProductUpdateDto>> Update(Guid productId, ProductUpdateDto productUpdateDto)
        {
            var response = new BaseResponse<ProductUpdateDto>();

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);
            if (product == null)
            {
                response.Status = false;
                response.Message = "Product not found";
                return response;
            }

            product.Name = productUpdateDto.Name;
            product.Description = productUpdateDto.Description;
            product.Price = productUpdateDto.Price;
            product.StockQuantity = productUpdateDto.StockQuantity;

            await _context.SaveChangesAsync();

            response.Data = productUpdateDto;
            response.Status = true;
            response.Message = "Success";
            return response;
        }
    }
}