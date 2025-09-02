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
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductCreateDto createDto)
        {
            var response = await _productService.Create(createDto);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAll();
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await _productService.GetById(id);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, ProductUpdateDto updateDto)
        {
            var response = await _productService.Update(id, updateDto);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _productService.Delete(id);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}