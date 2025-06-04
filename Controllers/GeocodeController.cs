using Microsoft.AspNetCore.Mvc;
using PTVGeocodingDemo.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PTVGeocodingDemo.Controllers
{
    public class GeocodeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeocodeController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["PTV:ApiKey"]; // get key from appsettings
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(GeocodeRequest model)
        {
            string requestUrl = $"https://api.myptv.com/geocoding/v1/locations/by-text?searchText={model.Address}";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("apiKey", _apiKey);

            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var location = doc.RootElement.GetProperty("locations")[0].GetProperty("referencePosition");

                model.Latitude = location.GetProperty("latitude").ToString();
                model.Longitude = location.GetProperty("longitude").ToString();
            }

            return View(model);
        }
    }
}

