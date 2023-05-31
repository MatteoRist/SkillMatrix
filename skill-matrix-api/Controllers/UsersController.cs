using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Models.Users;

namespace skill_matrix_api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            return Ok(DataMapper.MapToDto(UsersDataStore.Current.Users));
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> getUser(int id) {
            var userToReturn = UsersDataStore.Current.Users.FirstOrDefault(c => c.UserId == id);

            if(userToReturn == null) { return NotFound(); }

            return Ok(userToReturn);
        }
    }
}
