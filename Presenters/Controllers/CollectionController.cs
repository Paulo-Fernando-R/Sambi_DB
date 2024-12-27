using db.Index.Exceptions;
using db.Index.Operations;
using db.Presenters.Requests;
using Microsoft.AspNetCore.Mvc;

namespace db.Presenters.Controllers
{
    [ApiController]
    public class CollectionController : ControllerBase
    {

        private readonly CollectionOperations collectionOperations;

        public CollectionController(CollectionOperations collectionOperations)
        {
            this.collectionOperations = collectionOperations;
        }

        [HttpPost]
        [Route("[controller]/Create/{DatabaseName}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(string DatabaseName, CollectionRequest request)
        {
            try
            {
                collectionOperations.Create(DatabaseName, request);
                return Ok($"Collection '{request.CollectionName} created sucessfully");
            }
            catch (AlreadyExistsException ex)
            {

                return BadRequest(ex.Message);
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
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(string DatabaseName, CollectionUpdateRequest request)
        {
            if (request.Confirm != true)
            {
                return BadRequest("This operation requires a request confirmation");
            }

            try
            {
                collectionOperations.Update(DatabaseName, request.CollectionName, request.NewCollectionName);
                return Ok($"Collection '{request.CollectionName} Updated sucessfully");
            }
            catch (DirectoryNotExistsException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (AlreadyExistsException ex)
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
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(string DatabaseName, CollectionDeleteRequest request)
        {

            if (request.Confirm != true)
            {
                return BadRequest("This operation requires a request confirmation");
            }

            try
            {
                collectionOperations.Delete(DatabaseName, request.CollectionName);
                return Ok($"Collection '{request.CollectionName} deleted sucessfully");
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
    }
}
