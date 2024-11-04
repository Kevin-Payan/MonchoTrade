using Microsoft.AspNetCore.Mvc;
using monchotradebackend.Interface;
using monchotradebackend.models.entities;
using monchotradebackend.models.dtos;
using Microsoft.EntityFrameworkCore;
using monchotradebackend.service;
using Newtonsoft.Json;

namespace monchotradebackend.controllers 
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User, int> _dbRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IRepository<User, int> dbRepository,
            IPasswordHashingService passwordHashingService,
            ILogger<UserController> logger)
        {
            _dbRepository = dbRepository;
            _passwordHashingService = passwordHashingService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserMyProfileResponseDto>> GetUserById(int id)
        {
            try
            {
                var user = await _dbRepository.GetQueryable()
                    .Include(u => u.ProfileImage)
                    .Include(u => u.Products)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var userDto = new UserMyProfileResponseDto
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    SecondLastName = user.SecondLastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Country = user.Country,
                    ProfileImageUrl = user.ProfileImage.Url
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user data for ID: {Id}", id);
                return StatusCode(500, "An error occurred while fetching user data.");
            }
        }

           //Update/Put product 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] string jsonValues)
        {
            try
            {
                var existingUser = await _dbRepository.GetByIdAsync(id);
                if (existingUser == null)
                    return NotFound($"User with ID {id} not found.");

                JsonConvert.PopulateObject(jsonValues, existingUser);

                if (!TryValidateModel(existingUser))
                    return BadRequest(ModelState.GetFullErrorMessage());

                await _dbRepository.UpdateAsync(existingUser);
                await _dbRepository.SaveChangesAsync();

                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID {Id}.", id);
                return StatusCode(500, "An error occurred while updating user.");
            }
        }
    }
}