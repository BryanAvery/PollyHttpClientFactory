using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace PollyHttpClientFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        static int _getRequestCount = 0;
        static int _deleteRequestCount = 0;
        static int _postRequestCount = 0;

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            await Task.Delay(10000);// simulate some data by delaying for 10 seconds 
            _getRequestCount++;

            if (_getRequestCount % 4 == 0) // only one of out four requests will succeed
            {
                return Ok(_getRequestCount);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong when getting.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Task.Delay(100);// simulate some data by delaying for 100 milliseconds 

            _deleteRequestCount++;
            if (_deleteRequestCount % 4 == 0)
            {
                return Ok();
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong when deleting.");

        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Post(int id)
        {
            await Task.Delay(100);// simulate some data by delaying for 100 milliseconds 

            return Ok();

            //_postRequestCount++;
            //if (_postRequestCount % 4 == 0)
            //{
            //    return Ok();
            //}

            //return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong when deleting.");
        }

    }
}