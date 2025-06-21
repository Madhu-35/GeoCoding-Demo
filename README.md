# PTVGeocodingDemo
 PTV Geocoding Demo (ASP.NET Core MVC)
This project demonstrates the integration of the PTV Geocoding API with an ASP.NET Core MVC web application. Users can input any address to retrieve geographical coordinates and detailed address information.

🚀 Features:
Fetch geolocation details using PTV API
Display formatted address, latitude, longitude, street, city, state, country, etc.
Interactive map with Leaflet.js
Styled modern UI with custom CSS and particle animation
Address validation and reset functionality

🛠 Technologies Used:
ASP.NET Core MVC (.NET 8)
C#
Leaflet.js (for map rendering)
PTV Geocoding API
HTML / CSS / Bootstrap
JavaScript / jQuery
Particles.js (for animated background)

📂 Project Structure:
Controllers/GeocodeController.cs – Handles logic and API communication
Models/GeocodeInputModel.cs, GeocodeResultModel.cs, GeocodeViewModel.cs – Strongly typed view models
Views/Geocode/Index.cshtml – Unified view with address input and result display
