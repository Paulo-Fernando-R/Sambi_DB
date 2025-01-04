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
        public async Task<IActionResult> Create(string DatabaseName, RegisterCreateRequest request)
        {

            try
            {
                await registerOperations.Create(DatabaseName, request);
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
        public async Task<IActionResult> Update(string DatabaseName, RegisterUpdateRequest request)
        {

            try
            {
                await registerOperations.Update(DatabaseName, request);
                return Ok();
            }
            catch (DirectoryNotExistsException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete]
        [Route("[controller]/Delete/{DatabaseName}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string DatabaseName, RegisterDeleteRequest request)
        {
            if (request.Confirm != true)
            {
                return BadRequest("This operation requires a request confirmation");
            }

            try
            {
                await registerOperations.Delete(DatabaseName, request);
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

        [HttpPut]
        [Route("[controller]/Update/Array/{DatabaseName}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateArray(string DatabaseName, RegisterUpdateArrayRequest request)
        {
            if (request.Confirm != true)
            {
                return BadRequest("This operation requires a request confirmation");
            }

            try
            {
                int res = await registerOperations.UpdateArray(DatabaseName, request);
                return Ok($"Register '{request.RegisterId}:{request.ArrayName}' updated sucessfully into '{request.CollectionName}' collection.\n{res} itens affected");
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

        [HttpDelete]
        [Route("[controller]/Delete/Array/{DatabaseName}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteArray(string DatabaseName, RegisterDeleteArrayRequest request)
        {
            if (request.Confirm != true)
            {
                return BadRequest("This operation requires a request confirmation");
            }

            try
            {
                int res = await registerOperations.DeleteArray(DatabaseName, request);
                return Ok($"Register '{request.RegisterId}:{request.ArrayName}' deleted sucessfully into '{request.CollectionName}' collection.\n{res} itens affected");
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
