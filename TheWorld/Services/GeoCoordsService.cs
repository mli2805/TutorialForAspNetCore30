using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TheWorld.Services
{
    public class GeoCoordsService
    {
        private readonly ILogger<GeoCoordsService> _logger;
        private readonly IConfigurationRoot _config;

        public GeoCoordsService(ILogger<GeoCoordsService> logger, IConfigurationRoot config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<GeoCoordsResult> GetCoordsAsync(string name)
        {
            await Task.Delay(1);
            var result = new GeoCoordsResult()
            {
                Success = false,
                Message = "Failed to get coors",
            };

            var apiKey = _config["Keys:BingKey"];
            _logger.LogInformation($"getting coordinates from bing map where I have a key {apiKey}");

            //            var encodedName = WebUtility.UrlEncode(name);
            //            var url = $"http://dev.virtualearth/REST/v1/Locations?q={encodedName}&key={apiKey}";
            //
            //            var client = new HttpClient();
            //
            //            var json = await client.GetStringAsync(url);
            //
            //// Read out the results
            //// Fragile, might need to change if the Bing API changes
            //            var results = JObject.Parse(json);
            //            var resources = results["resourceSets"][0]["resources"];
            //            if (!results["resourceSets"][0]["resources"].HasValues)
            //            {
            //                result.Message = $"Could not find '{name}' as a location";
            //            }
            //            else
            //            {
            //                var confidence = (string)resources[0]["confidence"];
            //                if (confidence != "High")
            //                {
            //                    result.Message = $"Could not find a confident match for '{name}' as a location";
            //                }
            //                else
            //                {
            //                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
            //                    result.Latitude = (double) coords[0];
            //                    result.Longitude = (double) coords[1];
            //                    result.Success = true;
            //                    result.Message = "Success";
            //                    return result;
            //                }
            //            }

            result.Success = true;
            result.Message = "Success";
            result.Latitude = 32.5141231;
            result.Longitude = -78.79787079;

            return result;
        }
    }

    public class GeoCoordsResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
