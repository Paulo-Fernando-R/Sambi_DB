using db.Index.Exceptions;
using db.Index.Operations;
using db.Presenters.Requests;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace db.Presenters.Controllers
{
    [ApiController]
    public class QueryController : ControllerBase
    {


        private readonly ILogger<QueryController> _logger;
        private readonly QueryOperations queryOperations;

        public QueryController(ILogger<QueryController> logger)
        {
            _logger = logger;
            queryOperations = new QueryOperations();
        }



        [HttpPost]
        [Route("[controller]/QueryByProperties")]
        public IActionResult GetByProperties(QueryByPropertiesRequest request)
        {

            queryOperations.QueryByPropertiesFactory(request);
            return Ok();


        }

        [HttpPost]
        [Route("[controller]/QueryById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(QueryByIdRequest request)
        {
            try
            {
                var res = queryOperations.QueryById(request);
                return Content(res.Keys, "application/json");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
