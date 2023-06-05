using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Entities;
using skill_matrix_api.Services;

namespace skill_matrix_api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _dataStore;
        public UsersController(IUserRepository dataStore) 
        { 
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _dataStore.GetUsersAsync());
        }

        [HttpGet("{UserId}", Name = "GetUser")]
        public async Task<ActionResult<User>> GetUser(int UserID)
        {
            var userToReturn = await _dataStore.GetUserAsync(UserID);

            if(userToReturn == null) { return NotFound(); }

            return Ok(userToReturn);
        }
    }
}
