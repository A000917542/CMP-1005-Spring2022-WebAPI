using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CMP_1005_Spring2022_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("{temp}")]
        public WeatherForecast GetSingle(int temp)
        {
            var rng = new Random();
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(1),
                TemperatureC = temp,
                Summary = Summaries[rng.Next(Summaries.Length)]
            };
        }

        [HttpPost]
        public StatusCodeResult CreateForecast([FromBody] WeatherForecast forecast)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public StatusCodeResult DeleteForecast(int id)
        {
            return Ok();
        }


        [HttpPut]
        [Route("{id}")]
        public StatusCodeResult UpdateForecast(int id, [FromBody] WeatherForecast forecast)
        {
            return Ok();
        }

        [HttpHead]
        [Route("{temp}")]
        public StatusCodeResult GetForecastHead(int temp)
        {
            var rng = new Random();
            _ = new WeatherForecast
            {
                Date = DateTime.Now.AddDays(1),
                TemperatureC = temp,
                Summary = Summaries[rng.Next(Summaries.Length)]
            };
            return Ok();
        }

        //[HttpPatch]
        //[Route("{id}")]
        //public StatusCodeResult UpdatePartOfTheForecast(int id, [FromBody] WeatherForecast forecast)
        //{
        //    return Ok();
        //}

        [HttpOptions]
        public string OptionsForTheForecastController()
        {
            List<string> methods = new List<string>();

            MethodInfo[] methodArray = this.GetType().GetMethods();

            foreach (MethodInfo method in methodArray)
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(method);

                foreach (var attr in attrs)
                {
                    if (attr is HttpMethodAttribute)
                    {
                        Type t = attr.GetType();
                        string tName = t.Name.Replace("Http", "").Replace("Attribute", "").ToUpper();
                        if (!methods.Contains(tName))
                        {
                            methods.Add(tName);
                        }
                    }
                }
            }
            
            Response.Headers.Add("Allow", methods.ToArray());
            return "This is the Weather forecast controller. The methods available are 'Get', 'Post', and 'Put'.";
        }
    }
}
