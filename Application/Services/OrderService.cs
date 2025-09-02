using ECommerceApi.Application.DTOs;
using ECommerceApi.Application.Interfaces;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Application.Services
{
    public class OrderService(ApplicationContext context) : IOrderService
    {
        private readonly ApplicationContext _context = context;
        public async Task<BaseResponse<OrderCreateDto>> Create(OrderCreateDto createDto, Guid productId)
        {
            var response = new BaseResponse<OrderCreateDto>();

            var existingOrder = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.UserId == createDto.UserId && !o.IsCheckedOut);

            if (existingOrder is not null)
            {
                var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == productId);

                if (existingProduct is null)
                {
                    response.Message = "Product not found";
                    return response;
                }

                if (existingProduct.StockQuantity <= 0)
                {
                    response.Message = "Product is out of stock";
                    return response;
                }

                var existingOrderItem = existingOrder.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);

                if (existingOrderItem is not null)
                {
                    existingOrderItem.Quantity += 1;
                    existingOrderItem.SubTotal = existingOrderItem.Quantity * existingOrderItem.UnitPrice;
                    existingOrder.TotalAmount += existingOrderItem.UnitPrice;
                    existingProduct.StockQuantity -= 1;

                    try
                    {
                        await _context.SaveChangesAsync();
                        response.Status = true;
                        response.Message = "Added successfully";
                        return response;
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        response.Message = "An error occurred, Try again";
                        return response;
                    }
                }

                var newOrderItem = new OrderItem
                {
                    OrderId = existingOrder.Id,
                    ProductId = productId,
                    UnitPrice = existingProduct.Price,
                    SubTotal = existingProduct.Price,
                    Quantity = 1
                };


                existingProduct.StockQuantity -= 1;
                existingOrder.TotalAmount += existingProduct.Price;

                try
                {
                    await _context.OrderItems.AddAsync(newOrderItem);
                    await _context.SaveChangesAsync();
                    response.Status = true;
                    response.Message = "Order updated successfully";
                    return response;
                }
                catch (DbUpdateConcurrencyException)
                {
                    response.Message = "An error occurred, Try again";
                    return response;
                }
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);

            if (product is null)
            {
                response.Message = "Product not found";
                return response;
            }

            if (product.StockQuantity <= 0)
            {
                response.Message = "Product is out of stock";
                return response;
            }

            var order = new Order
            {
                UserId = createDto.UserId,
                TotalAmount = product.Price,
                OrderItems =
                {
                    new OrderItem
                    {
                        ProductId = productId,
                        UnitPrice = product.Price,
                        SubTotal = product.Price,
                        Quantity = 1
                    }
                },
            };

            product.StockQuantity -= 1;

            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Order created successfully";
                return response;
            }
            catch (DbUpdateConcurrencyException)
            {
                response.Message = "An error occurred, Try again";
                return response;
            }
        }

        public async Task<BaseResponse<bool>> Delete(Guid orderId)
        {
            var response = new BaseResponse<bool>();

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId && !o.IsCheckedOut && !o.IsDeleted);

            if (order is null)
            {
                response.Message = "Order not found";
                return response;
            }

            _context.Orders.Remove(order);

            try
            {
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Order deleted successfully";
            }
            catch (DbUpdateConcurrencyException)
            {
                response.Message = "An error occurred, Try again";
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<OrderListDto>>> GetAll()
        {
            var response = new BaseResponse<IEnumerable<OrderListDto>>();

            var orders = await _context.Orders.Include(o => o.OrderItems).Where(o => !o.IsCheckedOut && !o.IsDeleted).ToListAsync();
            if (orders is null || !orders.Any())
            {
                response.Message = "No orders found";
                return response;
            }

            response.Data = orders.Select(o => new OrderListDto
            {
                Id = o.Id,
                UserId = o.UserId,
                TotalAmount = o.TotalAmount,
                IsCheckedOut = o.IsCheckedOut,
                ItemCount = o.OrderItems.Count,
                OrderDate = o.OrderDate
            });

            response.Status = true;
            response.Message = "Success";
            return response;
        }

        public async Task<BaseResponse<OrderResponseDto>> GetById(Guid orderId)
        {
            var response = new BaseResponse<OrderResponseDto>();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId && !o.IsDeleted);

            if (order is null)
            {
                response.Message = "Order not found";
                return response;
            }

            response.Data = new OrderResponseDto
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                OrderItems = [.. order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    Id = oi.Id,
                    ProductName = oi.Product?.Name,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity,
                    SubTotal = oi.SubTotal
                })]
            };

            response.Status = true;
            response.Message = "Success";
            return response;
        }

        public async Task<BaseResponse<OrderResponseDto>> GetUserActiveCart(Guid userId)
        {
            var response = new BaseResponse<OrderResponseDto>();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.UserId == userId && !o.IsCheckedOut && !o.IsDeleted);

            if (order is null)
            {
                response.Message = "No uncompleted order found";
                return response;
            }

            response.Data = new OrderResponseDto
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                OrderItems = [.. order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    Id = oi.Id,
                    ProductName = oi.Product?.Name,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity,
                    SubTotal = oi.SubTotal
                })]
            };

            response.Status = true;
            response.Message = "Success";
            return response;
        }
    }
}