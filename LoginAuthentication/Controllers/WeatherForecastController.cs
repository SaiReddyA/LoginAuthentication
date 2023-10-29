using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LoginAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private string _randomNumber = "";
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
        [Route("SendVerification")]
        public ActionResult SendVerification()
        {
            try
            {
                String result;
                string aPIKey = "NTM0ODU0NDg2ODRjNjQ0YTY4MzM0YTY0NzY1NTUzN2E=";
                string number = "9398712562";
                string name = "Sai Redddy";
                Random random = new Random();
                _randomNumber = (random.Next(1000, 9999)).ToString();
                string message = "Hey " + name + " your OTP is " + _randomNumber;
                string sender = "SaiReddySoftwateSolutions";

                String url = "https://api.txtlocal.com/send/?apikey=" + aPIKey + "&numbers=" + number + "&message=" + message + "&sender=" + sender;
                //refer to parameters to complete correct url string

                StreamWriter myWriter = null;
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

                objRequest.Method = "POST";
                objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
                objRequest.ContentType = "application/x-www-form-urlencoded";
                try
                {
                    myWriter = new StreamWriter(objRequest.GetRequestStream());
                    myWriter.Write(url);
                }
                catch 
                {
                    throw;
                }
                finally
                {
                    myWriter.Close();
                }

                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    // Close and clean up the StreamReader
                    sr.Close();
                }
                return Ok(result);
            

            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("VerifyOTP")]
        public ActionResult VerifyOTP(string OTP)
        {
            try
            {
                if(_randomNumber == OTP)
                {
                    return Ok("Success");
                }
                else
                {
                    return Ok("Not Matched");
                }
            }catch
            {
                throw;
            }
        }


    }
}
