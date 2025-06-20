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
            if (string.IsNullOrWhiteSpace(model.Address))
            {
                ModelState.AddModelError("Address", "Please enter a valid address.");
                return View(model);
            }

            try
            {
                string requestUrl = $"https://api.myptv.com/geocoding/v1/locations/by-text?searchText={model.Address}";

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("apiKey", _apiKey);

                var response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    using var doc = JsonDocument.Parse(json);
                  
                    var locations = doc.RootElement.GetProperty("locations");

                    if (locations.GetArrayLength() > 0)
                    {
                        var location = locations[0].GetProperty("referencePosition");
                        model.Latitude = location.GetProperty("latitude").ToString();
                        model.Longitude = location.GetProperty("longitude").ToString();
                        ModelState.Clear(); // ✅ clears earlier validation errors
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "No location found for the entered address.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"API Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while contacting the geocoding service: " + ex.Message);
            }
            catch (JsonException ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing the geocoding response: " + ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred: " + ex.Message);
                // Optional: log ex for debugging
            }
            return View(model);
        }
    }
}

