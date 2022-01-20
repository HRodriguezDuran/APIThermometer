using ApiThermometer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiThermometer.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class ThermometerController : Controller
    {
        private readonly ILogger<ThermometerController> _logger;

        public double Freezing { get; set; }
        public double Boiling { get; set; }

        public bool showFreezingMessage = true;
        public bool showBoilingMessage = true;
        public bool showTemperatureMessage = true;

        public bool Celsius = true;

        public ThermometerController(ILogger<ThermometerController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetListTemperature")]
        public dynamic GetListTemperature()
        {
            try
            {
                var thermometer = new Thermometer();
                var ListTemperatures = thermometer.GetTemperature();
                return ListTemperatures;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        

        [HttpGet(Name = "GetThermometer")]

        public Response GetThermometer(double freezing, double boiling, bool celsius)
        {
            Response response = new Response { Message = "" };
            Freezing = freezing;
            Boiling = boiling;
            Celsius = celsius;

            var thermometer = new Thermometer();


            var ListTemperatures = thermometer.GetTemperature();

            if (celsius)
            {
                for (int i = 0; i < ListTemperatures.Count(); i++)

                {
                    if (((i > 0 && ListTemperatures[i - 1].TemperatureC > ListTemperatures[i].TemperatureC) && ListTemperatures[i].TemperatureC <= (freezing + 0.5) && showFreezingMessage)
                        || ((i == 0) && ListTemperatures[i].TemperatureC <= (freezing + 0.5) && showFreezingMessage))
                    {
                        response.Message += "The temperature is: " + ListTemperatures[i].TemperatureC + " C  freezing temperature reached;  ";
                        showFreezingMessage = false;
                        showTemperatureMessage = false;
                    }
                    if (((i > 0 && ListTemperatures[i - 1].TemperatureC < ListTemperatures[i].TemperatureC) && ListTemperatures[i].TemperatureC >= (boiling - 0.5) && showBoilingMessage)
                        || ((i == 0) && ListTemperatures[i].TemperatureC >= (boiling - 0.5) && showBoilingMessage))
                    {
                        response.Message += "The temperature is: " + ListTemperatures[i].TemperatureC + " C  boiling temperature reached;  ";
                        showBoilingMessage = false;
                        showTemperatureMessage = false;
                    }
                    if (showTemperatureMessage)
                    {
                        response.Message += "The temperature is: " + ListTemperatures[i].TemperatureC + " C;  ";
                    }

                    showTemperatureMessage = true;
                }

            }

            else
            {
                for (int i = 0; i < ListTemperatures.Count(); i++)

                {
                    if (((i > 0 && ListTemperatures[i - 1].TemperatureF > ListTemperatures[i].TemperatureF) && ListTemperatures[i].TemperatureF <= (freezing + 0.5) && showFreezingMessage)
                        || ((i == 0) && ListTemperatures[i].TemperatureF <= (freezing + 0.5) && showFreezingMessage))
                    {
                        response.Message += "The temperature is: " + ListTemperatures[i].TemperatureF + " F  freezing temperature reached;  ";
                        showFreezingMessage = false;
                        showTemperatureMessage = false;
                    }
                    if (((i > 0 && ListTemperatures[i - 1].TemperatureF < ListTemperatures[i].TemperatureF) && ListTemperatures[i].TemperatureF >= (boiling - 0.5) && showBoilingMessage)
                        || ((i == 0) && ListTemperatures[i].TemperatureF >= (boiling - 0.5) && showBoilingMessage))
                    {
                        response.Message += "The temperature is: " + ListTemperatures[i].TemperatureF + " F  boiling temperature reached;  ";
                        showBoilingMessage = false;
                        showTemperatureMessage = false;
                    }
                    if (showTemperatureMessage)
                    {
                        response.Message += "The temperature is: " + ListTemperatures[i].TemperatureF + " F;  ";
                    }

                    showTemperatureMessage = true;
                }

            }

            return response;
        }



    }
}
