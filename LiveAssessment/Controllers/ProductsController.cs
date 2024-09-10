 
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using LiveAssessment.Dto;
using LiveAssessment.Helper;
using LiveAssessment.Interfaces;
using LiveAssessment.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace WashAndGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IVariationRepository _variationRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProductsController(IProductRepository productRepository, IVariationRepository variationRepository, IMapper mapper, IConfiguration configuration)
        {
            _productRepository = productRepository;
            _variationRepository = variationRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(200)]
        public IActionResult GetAdmin(int Id)
        {
            if (Id == 0)                 
                return BadRequest(new { message = " ID required." });

            try
            {
                 
                var product = _mapper.Map<ProductDto>(_productRepository.GetProductById(Id)); // Make sure your mapping is configured for productDto to Product


                if (product == null)
                {
                    return NoContent(); // 204 No Content
                }

                return Ok(product);

            }
            catch (System.Exception ex)
            {
                Console.Error.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int Id,[FromBody] ProductDto productDto)
        {

            try
            {
                if (productDto == null)
                    return BadRequest(new { message = "productDto details required." });

                var foundProduct = _productRepository.GetProductById(Id);

                if (foundProduct != null)
                {
                    foundProduct.Name = productDto.Name;
                    foundProduct.Description = productDto.Description;
                    foundProduct.Price = productDto.Price;
                    foundProduct.Sku = productDto.Sku;
                    foundProduct.VendorId = productDto.VendorId;

                    foundProduct.Variations = _mapper.Map<ICollection<Variation>>(productDto.Variations);
                     
                     

                    if (!ModelState.IsValid)
                        return BadRequest();

                    if (!_productRepository.UpdateProduct(foundProduct))
                    {
                        ModelState.AddModelError("", "Something went wrong updating the Product");
                        return StatusCode(500, ModelState);
                    }

                    return NoContent();
                }
                else
                {
                    return NotFound(new { message = "Product not found." });
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest(new { message = "Product details required." });

            try
            {
                

                var Product = _mapper.Map<Product>(productDto); // Make sure your mapping is configured for productDto to Product

                if (_productRepository.CreateProduct(Product))
                {
                    return StatusCode(StatusCodes.Status201Created, new { success = $"Product created!" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the Product." });
                }
            }
            catch (System.Exception ex)
            {
                Console.Error.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }



    }
}
