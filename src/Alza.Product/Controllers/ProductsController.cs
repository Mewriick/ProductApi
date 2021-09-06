﻿using Alza.Product.Application.Dtos;
using Alza.Product.Application.Patch;
using Alza.Product.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alza.Product.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;


        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Endpoint which returns products from catalog
        /// </summary>
        /// <param name="skip">How many products we want skip while retrieving</param>
        /// <param name="take">How many products we want retrieve</param>
        /// <returns>Collection of products</returns>
        /// <response code="200">Returns the products collection</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int skip = 0, int take = int.MaxValue)
        {
            var products = await mediator.Send(
                new GetAllProducts(skip, take),
                HttpContext.RequestAborted);

            return Ok(products);
        }

        /// <summary>
        /// Endpoint which returs single product from catalog
        /// </summary>
        /// <param name="productId">Product unique identifier</param>
        /// <returns>Single product</returns>
        /// <response code="200">Returns the product</response>
        /// <response code="204">Returns the product if not exists in system</response>
        [HttpGet("Product/{productId}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetById(Guid productId)
        {
            var product = await mediator.Send(
                new GetProductById(productId),
                HttpContext.RequestAborted);

            if (product is not null)
            {
                return Ok(product);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPatch]
        [Route("Patch/Product/{productId}")]
        public async Task<IActionResult> Patch(Guid productId, [FromBody] JsonMergePatch<ProductDto> request)
        {
            return Ok();
        }
    }
}

