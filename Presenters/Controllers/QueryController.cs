using db.Index.Enums;
using db.Index.Exceptions;
using db.Index.Operations;
using db.Presenters.Requests;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace db.Presenters.Controllers
{
    [ApiController]
    public class QueryController : ControllerBase
    {


        private readonly ILogger<QueryController> _logger;
        private readonly QueryOperations queryOperations;

        public QueryController(ILogger<QueryController> logger, QueryOperations queryOperations)
        {
            _logger = logger;
            this.queryOperations = queryOperations;
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



        [HttpPost]
        [Route("[controller]/QueryByProperty/AND")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByPropertyAND(QueryByPropertiesRequest request)
        {

            try
            {
                var res = queryOperations.QueryByPropertyOpAND(OperatorsEnum.And.ToDescriptionString(), request);

                return Content(JsonConvert.SerializeObject(res), "application/json");
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
            catch (OperationNotAllowedException ex)
            {
                return BadRequest(ex);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }


        }

        [HttpPost]
        [Route("[controller]/QueryByProperty/OR")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByPropertyOr(QueryByPropertiesRequest request)
        {



            try
            {
                var res = queryOperations.QueryByPropertyOpAND(OperatorsEnum.Or.ToDescriptionString(), request);

                return Content(JsonConvert.SerializeObject(res), "application/json");
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
                return BadRequest(ex.Message);
            }
            catch (OperationNotAllowedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
