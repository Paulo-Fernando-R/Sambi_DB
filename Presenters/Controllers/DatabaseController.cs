using db.Index.Exceptions;
using db.Index.Operations;
using db.Presenters.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace db.Presenters.Controllers
{
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DatabaseController> _logger;
        private readonly DatabaseOperations databaseOperations;

        public DatabaseController(ILogger<DatabaseController> logger, IConfiguration configuration, DatabaseOperations databaseOperations)
        {
            _logger = logger;
            _configuration = configuration;
            this.databaseOperations = databaseOperations;
        }

        [HttpPost]
        [Route("[controller]/Create")]
        public IActionResult Create(DatabaseCreateRequest request)
        {
            try
            {
                databaseOperations.DatabaseCreate(request);
                return Ok();
            }
            catch (AlreadyExistsException ex)
            {

               return BadRequest(ex.Message);
            }
        }
    }
}
