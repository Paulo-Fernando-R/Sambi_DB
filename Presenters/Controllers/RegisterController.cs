using db.Index.Exceptions;
using db.Index.Operations;
using db.Presenters.Requests;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("[controller]/Delete/{DatabaseName}")]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}
