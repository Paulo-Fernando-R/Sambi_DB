using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace db.Presenters.Controllers
{
    [ApiController]
    public class SambiAdminController : ControllerBase
    {
        [HttpGet]
        [Route("[controller]/")]
        [Produces("text/html")]
        public IActionResult Admin()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "wwwroot", "index.html");
            return PhysicalFile(path, "text/html");
        }
    }
}
