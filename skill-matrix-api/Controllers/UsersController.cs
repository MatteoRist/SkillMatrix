using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Entities;
using skill_matrix_api.Services;

namespace skill_matrix_api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _dataStore;
        public UsersController(IUserRepository dataStore) 
        { 
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        /// <summary>
        /// Retrieves a list of users.
        /// </summary>
        /// <returns>An action result containing the list of users.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _dataStore.GetUsersAsync());
        }

        /// <summary>
        /// Retrieves a specific user by its ID.
        /// </summary>
        /// <param name="UserID">The ID of the user to retrieve.</param>
        /// <returns>An action result containing the retrieved user.</returns>
        [HttpGet("{UserId}", Name = "GetUser")]
        public async Task<ActionResult<User>> GetUser(int UserID)
        {
            var user = await _dataStore.GetUserAsync(UserID);

            if(user == null) { return NotFound(); }

            return Ok(user);
        }

        [HttpGet("{UserId}/statistics")]
        public async Task<ActionResult<ICollection<Statistic>>> GetUserStatistic(int UserID)
        {
            var stats = await _dataStore.GetUserStatisticsAsync(UserID);

            return Ok(stats);
        }
    }
}
