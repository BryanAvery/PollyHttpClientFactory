using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyHttpClientFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogueController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CatalogueController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            string requestEndpoint = $"inventory/{id}";

            var httpClient = _httpClientFactory.CreateClient("RemoteServer");

            HttpResponseMessage response = await httpClient.GetAsync(requestEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var itemsInStock = await response.Content.ReadAsStringAsync();
                return Ok(itemsInStock);
            }

            return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            string requestEndpoint = $"inventory/{id}";

            var httpClient = _httpClientFactory.CreateClient("RemoteServer");

            HttpResponseMessage response = await httpClient.DeleteAsync(requestEndpoint);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Post(int id)
        {
            string requestEndpoint = $"inventory/{id}";

            var httpClient = _httpClientFactory.CreateClient("RemoteServer");

            StringContent queryString = new StringContent(id.ToString());

            HttpResponseMessage response = await httpClient.PostAsync(requestEndpoint, queryString);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync());
        }
    }
}