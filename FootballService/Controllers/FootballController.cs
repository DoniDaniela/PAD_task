using FootballService.Modules;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace FootballService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FootballController : Controller
    {
        private readonly ILogger<FootballController> _logger;
        private readonly IConfiguration _confog;

        public FootballController(ILogger<FootballController> logger, IConfiguration config)
        {
            _logger = logger;
            _confog = config;
        }

        [HttpGet(Name = "predictions")]
        public async Task<string> GetPredictions(string markets, string iso_date, string federation)
        {
            try
            {
                var options = new RestClientOptions("https://football-prediction-api.p.rapidapi.com/api/v2/")
                {
                    MaxTimeout = 30000,
                };

                var client = new RestClient(options);
                var request = new RestRequest("predictions");

                request.AddHeader("X-RapidAPI-Key", "6f15c64f73msh0dbb6e8111bbbcfp1abae6jsnf3a47405575f");
                request.AddHeader("X-RapidAPI-Host", "football-prediction-api.p.rapidapi.com");

                request.AddParameter("market", markets);
                request.AddParameter("iso_date", iso_date);
                request.AddParameter("federation", federation);

                var response = client.GetAsync(request).Result;

                _logger.LogDebug($"Response: {response.IsSuccessful}, {response.Content}");
                DbUtils.SetLog(_confog["ConnectionStrings:Database"], response.ResponseUri?.ToString(), (int)response.StatusCode, response.Content);

                if (response.IsSuccessful)
                {
                    return response.Content;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        [HttpGet(Name = "predictionDetails")]
        public async Task<string> GetPredictionsDetails()
        {
            try
            {
                var options = new RestClientOptions("https://football-prediction-api.p.rapidapi.com/api/v2/")
                {
                    MaxTimeout = 30000,
                };

                var client = new RestClient(options);
                var request = new RestRequest("predictions/99999");

                request.AddHeader("X-RapidAPI-Key", "6f15c64f73msh0dbb6e8111bbbcfp1abae6jsnf3a47405575f");
                request.AddHeader("X-RapidAPI-Host", "football-prediction-api.p.rapidapi.com");

                var response = client.GetAsync(request).Result;

                _logger.LogDebug($"Response: {response.IsSuccessful}, {response.Content}");
                DbUtils.SetLog(_confog["ConnectionStrings:Database"], response.ResponseUri?.ToString(), (int)response.StatusCode, response.Content);

                if (response.IsSuccessful)
                {
                    return response.Content;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        [HttpGet(Name = "listFederations")]
        public async Task<string> GetListAvailableFederations()
        {
            try
            {
                var options = new RestClientOptions("https://football-prediction-api.p.rapidapi.com/api/v2/")
                {
                    MaxTimeout = 30000,
                };

                var client = new RestClient(options);
                var request = new RestRequest("list-federations");

                request.AddHeader("X-RapidAPI-Key", "6f15c64f73msh0dbb6e8111bbbcfp1abae6jsnf3a47405575f");
                request.AddHeader("X-RapidAPI-Host", "football-prediction-api.p.rapidapi.com");

                var response = client.GetAsync(request).Result;

                _logger.LogDebug($"Response: {response.IsSuccessful}, {response.Content}");
                DbUtils.SetLog(_confog["ConnectionStrings:Database"], response.ResponseUri?.ToString(), (int)response.StatusCode, response.Content);

                if (response.IsSuccessful)
                {
                    return response.Content;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        [HttpGet(Name = "listMarkets")]
        public async Task<string> GetListAvailableMarkets()
        {
            try
            {
                var options = new RestClientOptions("https://football-prediction-api.p.rapidapi.com/api/v2/")
                {
                    MaxTimeout = 30000,
                };

                var client = new RestClient(options);
                var request = new RestRequest("list-markets");

                request.AddHeader("X-RapidAPI-Key", "6f15c64f73msh0dbb6e8111bbbcfp1abae6jsnf3a47405575f");
                request.AddHeader("X-RapidAPI-Host", "football-prediction-api.p.rapidapi.com");

                var response = client.GetAsync(request).Result;

                _logger.LogDebug($"Response: {response.IsSuccessful}, {response.Content}");
                DbUtils.SetLog(_confog["ConnectionStrings:Database"], response.ResponseUri?.ToString(), (int)response.StatusCode, response.Content);

                if (response.IsSuccessful)
                {
                    return response.Content;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        [HttpGet("status", Name = "status")]
        public IActionResult GetStatus()
        {
            try
            {
                DbUtils.SetLog(_confog["ConnectionStrings:Database"], "TEST", 200, "Ok");
                return Ok("Service is running");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
