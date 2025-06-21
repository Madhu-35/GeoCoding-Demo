
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PTVGeocodingDemo.Models
{
    public class GeocodeRequest
    {
       
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }

        // Coordinates
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        // Detailed Address Information
        [ValidateNever]
        public string? FormattedAddress { get; set; }
        [ValidateNever]
        public string? Street { get; set; }
        [ValidateNever]
        public string? PostalCode { get; set; }
        [ValidateNever]
        public string? City { get; set; }
        [ValidateNever]
        public string? State { get; set; }
        [ValidateNever]
        public string? Country { get; set; }
    }
}
