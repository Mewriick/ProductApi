
using System;
using System.ComponentModel.DataAnnotations;

namespace Alza.Product.Application.Dtos
{
    /// <summary>
    /// Main product information
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Unique ideintifier of product
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Product friendly name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Product eshop description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Product relative path to the main image
        /// </summary>
        [Required]
        public string ImageUri { get; set; }
    }
}
