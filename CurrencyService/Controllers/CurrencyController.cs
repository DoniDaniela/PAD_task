using CurrencyService.Modules;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CurrencyService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CurrencyController : Controller
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly IConfiguration _confog;

        public CurrencyController(ILogger<CurrencyController> logger, IConfiguration config)
        {
            _logger = logger;
            _confog = config;
        }

        [HttpGet(Name = "currencies")]
        public async Task<string> GetCurrencies(string output)
        {
            try
            {
                var options = new RestClientOptions("https://currencyapi-net.p.rapidapi.com")
                {
                    MaxTimeout = 30000,
                };

                var client = new RestClient(options);
                var request = new RestRequest("currencies");

                request.AddHeader("X-RapidAPI-Key", "6f15c64f73msh0dbb6e8111bbbcfp1abae6jsnf3a47405575f");
                request.AddHeader("X-RapidAPI-Host", "currencyapi-net.p.rapidapi.com");

                request.AddParameter("output", output);

                var response = client.GetAsync(request).Result;

                _logger.LogDebug($"Response: {response.IsSuccessful}, {response.Content}");
                //DbUtils.SetLog(_confog["ConnectionStrings:Database"], response.ResponseUri?.ToString(), (int)response.StatusCode, response.Content);

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

        [HttpGet(Name = "convert")]

        public async Task<string> GetConvert(string from, string to, int amount)
        {
            try
            {
                var options = new RestClientOptions("https://currency-converter18.p.rapidapi.com/api/v1/")
                {
                    MaxTimeout = 30000,
                };

                var client = new RestClient(options);
                var request = new RestRequest("convert");

                request.AddHeader("X-RapidAPI-Key", "6f15c64f73msh0dbb6e8111bbbcfp1abae6jsnf3a47405575f");
                request.AddHeader("X-RapidAPI-Host", "currency-converter18.p.rapidapi.com");

                request.AddParameter("from", from);
                request.AddParameter("to", to);
                request.AddParameter("amount", amount);

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

        [HttpGet(Name = "rates")]

        public async Task<string> GetRates(string output, string currency)
        {
            try
            {
                var options = new RestClientOptions("https://currencyapi-net.p.rapidapi.com/")
                {
                    MaxTimeout = 30000,
                };

                var client = new RestClient(options);
                var request = new RestRequest("rates");

                request.AddHeader("X-RapidAPI-Key", "6f15c64f73msh0dbb6e8111bbbcfp1abae6jsnf3a47405575f");
                request.AddHeader("X-RapidAPI-Host", "currencyapi-net.p.rapidapi.com");

                request.AddParameter("output", output);
                request.AddParameter("base", currency);

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

        [HttpGet(Name = "history")]

        public async Task<string> GetHistory(string from, string to)
        {
            try
            {
                var options = new RestClientOptions("https://currency-conversion-and-exchange-rates.p.rapidapi.com/")
                {
                    MaxTimeout = 30000,
                };

                var client = new RestClient(options);
                var request = new RestRequest("2019-10-16?");

                request.AddHeader("X-RapidAPI-Key", "6f15c64f73msh0dbb6e8111bbbcfp1abae6jsnf3a47405575f");
                request.AddHeader("X-RapidAPI-Host", "currency-conversion-and-exchange-rates.p.rapidapi.com");

                request.AddParameter("from", from);
                request.AddParameter("to", to);

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
