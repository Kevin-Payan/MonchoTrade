
using Microsoft.AspNetCore.Mvc;
using monchotradebackend.Interface; 
using monchotradebackend.models.entities; 
using monchotradebackend.models.dtos; 
using Microsoft.EntityFrameworkCore;
using monchotradebackend.service;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace monchotradebackend.controllers
{

    public static class ModelStateExtensions
    {
    public static string GetFullErrorMessage(this ModelStateDictionary modelState)
    {
        var messages = new List<string>();

        foreach (var entry in modelState)
        {
            foreach (var error in entry.Value.Errors)
            {
                messages.Add(error.ErrorMessage);
            }
        }

        return string.Join(" ", messages);
    }
    }


    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IRepository<Product, int> _dbRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IFileService fileService, 
            IRepository<Product, int> dbRepository, 
            ILogger<ProductController> logger)
        {
            _fileService = fileService;
            _dbRepository = dbRepository;
            _logger = logger;
        }

        //Get all products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            try
            {
                var products = await _dbRepository.GetQueryable()
                    .Include(u => u.User)
                    .Include(i => i.Images)
                    .Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Title = p.Name,
                        ImageUrl = p.Images.FirstOrDefault().Url,
                        OfferedBy = p.User != null ? p.User.Name : "",
                        Description = p.Description,
                        Category = p.Category
                    }).ToListAsync();

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products data.");
                return StatusCode(500, "An error occurred while fetching products data.");
            }
        }

        // GET product by id 
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductByID(int id)
    {
        try
        {
            var product = await _dbRepository.GetQueryable()
                .Include(p => p.User)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Assuming you're mapping the product to a DTO
            var productDto = new ProductDto
            {
                Id = product.Id,
                Title = product.Name,
                ImageUrl = product.Images.FirstOrDefault()?.Url ?? "",  // Use null-checking for Images collection
                OfferedBy = product.User?.Name ?? "",  // Use null-checking for User
                Description = product.Description,
                Category = product.Category
            };

            return Ok(productDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching product data.");
            return StatusCode(500, "An error occurred while fetching product data.");
        }
    }


        //Create/Post product 
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product  newProduct)
        {
            try
            {
                bool exists = await _dbRepository.GetQueryable()
                    .AnyAsync(s => s.Id == newProduct.Id);

                if (exists)
                {
                    return BadRequest("Id del salario ya está registrado.");
                }

                await _dbRepository.InsertAsync(newProduct);
                await _dbRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new product.");
                return StatusCode(500, "An error occurred while creating new product.");
            }
        }

        //Update/Put product 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] string jsonValues)
        {
            try
            {
                var product = await _dbRepository.GetByIdAsync(id);
                if (product == null)
                    return NotFound($"Product with ID {id} not found.");

                JsonConvert.PopulateObject(jsonValues, product);

                if (!TryValidateModel(product))
                    return BadRequest(ModelState.GetFullErrorMessage());

                await _dbRepository.UpdateAsync(product);
                await _dbRepository.SaveChangesAsync();

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID {Id}.", id);
                return StatusCode(500, "An error occurred while updating product.");
            }
        }


        //Delete product
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _dbRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                await _dbRepository.DeleteAsync(product);
                await _dbRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with ID {Id}.", id);
                return StatusCode(500, "An error occurred while deleting product.");
            }
        }


    }


}