using Microsoft.AspNetCore.Mvc;

namespace skill_matrix_api.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home/Error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error()
        {
            return Problem("An error occurred while processing your request. Please try again later.");
        }
    }

}
