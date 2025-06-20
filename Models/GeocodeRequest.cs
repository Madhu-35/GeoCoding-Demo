using System.ComponentModel.DataAnnotations;

namespace PTVGeocodingDemo.Models
{
    public class GeocodeRequest
    {

        [Required(ErrorMessage = "Address is required")] 
        public string Address { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
