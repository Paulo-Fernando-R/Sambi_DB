using db.Index.Exceptions;
using db.Index.Operations;
using db.Presenters.Requests;
using Microsoft.AspNetCore.Mvc;

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
                return Ok($"Database '{request.DatabaseName}' was created sucessfully");
            }
            catch (AlreadyExistsException ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("[controller]/Delete/{DatabaseName}")]
        public IActionResult Delete(string DatabaseName, DatabaseDeleteRequest request)
        {
            if (request == null || request.Confirm != true)
            {
                return BadRequest("This operation requires a request confirmation");
            }

            try
            {
                databaseOperations.DatabaseDelete(DatabaseName);
                return Ok($"Database '{DatabaseName}' was deleted sucessfully");
            }
            catch (DirectoryNotExistsException ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
