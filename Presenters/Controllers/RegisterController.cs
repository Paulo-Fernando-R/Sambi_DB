using db.Index.Exceptions;
using db.Index.Operations;
using db.Presenters.Requests;
using Microsoft.AspNetCore.Mvc;

namespace db.Presenters.Controllers
{
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private readonly RegisterOperations registerOperations;

        public RegisterController(RegisterOperations registerOperations)
        {
            this.registerOperations = registerOperations;
        }

        [HttpPost]
        [Route("[controller]/Create/{DatabaseName}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(string DatabaseName, RegisterCreateRequest request)
        {

            try
            {
                registerOperations.Create(DatabaseName, request);
                return Ok($"Register added sucessfully into '{request.CollectionName}' collection");
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

        [HttpPut]
        [Route("[controller]/Update/{DatabaseName}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("[controller]/Delete/{DatabaseName}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(string DatabaseName, RegisterDeleteRequest request)
        {
            try
            {
                registerOperations.Delete(DatabaseName, request);
                return Ok($"Register '{request.RegisterId}' deleted sucessfully into '{request.CollectionName}' collection");
            }
            catch (DirectoryNotExistsException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
