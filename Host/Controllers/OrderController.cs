using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApi.Application.DTOs;
using ECommerceApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [HttpPost("Create/{productId}")]
        public async Task<IActionResult> Create(OrderCreateDto createDto, [FromRoute] Guid productId)
        {
            var response = await _orderService.Create(createDto, productId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Delete/{orderId}")]
        public async Task<IActionResult> Delete(Guid orderId)
        {
            var response = await _orderService.Delete(orderId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetUserActiveCart/{userId}")]
        public async Task<IActionResult> GetUserActiveCart(Guid userId)
        {
            var response = await _orderService.GetUserActiveCart(userId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _orderService.GetAll();
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetById/{orderId}")]
        public async Task<IActionResult> GetById(Guid orderId)
        {
            var response = await _orderService.GetById(orderId);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}