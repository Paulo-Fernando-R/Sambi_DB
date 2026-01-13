using db.Index.Exceptions;
using db.Index.Operations;
using db.Presenters.Requests;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet]
        [Route("[controller]/List")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> List()
        {
            try
            {
                var res = databaseOperations.DatabaseList();
                return Content(JsonConvert.SerializeObject(res), "application/json");
            }
            catch (DirectoryNotExistsException e)
            {
                return NotFound(e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
           
        }

        [HttpGet]
        [Route("[controller]/teste")]
        public IActionResult testc()
        {
            var tal = new HtmlString("<html>\r\n<title>HTML Tutorial</title>\r\n<body>\r\n\r\n<h1>This is a heading</h1>\r\n<p>This is a paragraph.</p>\r\n\r\n</body>\r\n</html>");
            return Content(tal.Value, "text/html");
        }
    }
}
