using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetPalsProfile.API.Contracts;
using PetPalsProfile.Domain.Abstractions;
using PetPalsProfile.Domain.Models;

namespace PetPalsProfile.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUsersService usersService, ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUser() {

            List<User> response = await _usersService.GetAllUsers();
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserPostRequest request)
        {
            (User user, string error) = Domain.Models.User.Create(
                Guid.NewGuid(),
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                request.Login);

            if (!string.IsNullOrEmpty(error))
            {
                BadRequest(error);
            }

            Guid userId = await _usersService.CreateUser(user);

            return Ok(userId);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Guid>> UpdateUser(Guid id, [FromBody] User request)
        {
            Guid userId = await _usersService.UpdateUser(
                id,
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                request.Description,
                request.Birthdate);

            return Ok(userId);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Guid>> DeleteUser(Guid id)
        {
            Guid userId = await _usersService.DeleteUser(id);
            _logger.LogInformation("КРЯ");
            return Ok(userId);
        }
    }

}
