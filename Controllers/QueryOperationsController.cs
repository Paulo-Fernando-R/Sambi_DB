using Microsoft.AspNetCore.Mvc;

namespace db.Controllers
{
    [ApiController]
    public class QueryOperationsController : ControllerBase
    {


        private readonly ILogger<QueryOperationsController> _logger;

        public QueryOperationsController(ILogger<QueryOperationsController> logger)
        {
            _logger = logger;
        }



        [HttpPost]
        [Route("[controller]/QueryByProperties")]
        public IActionResult GetByProperties()
        {

            return Ok();
        }

        [HttpGet]
        [Route("[controller]/QueryById/{CollectionName}/{RegisterId}")]
        public IActionResult GetById(string CollectionName, string RegisterId)
        {
            return Ok(new { CollectionName, RegisterId });
            
        }



    }
}
