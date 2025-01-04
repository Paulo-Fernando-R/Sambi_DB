using db.Index.Exceptions;
using db.Index.Operations;
using db.Presenters.Requests;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace db.Presenters.Controllers
{
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [Route("[controller]/ById/{DatabaseName}")]
        
        public async Task<IActionResult> GetById(string DatabaseName, QueryByIdRequest request)
        {
            try
            //Adicionar um midleware para gerenciar as excessões ao invés de fazer diretamente no controller
            {
                var res = await queryOperations.QueryById(DatabaseName, request);
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
            catch (DirectoryNotExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("[controller]/ByProperty/{DatabaseName}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByPropertyAND(string DatabaseName, QueryByPropertiesRequest request)
        {
           
            try
            {
                var res = await queryOperations.QueryByProperty(DatabaseName, request);

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
            catch (DirectoryNotExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

    }
}
