using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace Spike.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetWeather(decimal latitude, decimal longitude)
        {
            return Json(GetWeatherInfo(latitude, longitude), JsonRequestBehavior.AllowGet);
        }

        private WeatherInfo GetWeatherInfo(decimal latitude, decimal longitude)
        {
            var jsonString = GetWeatherInfoApi(latitude, longitude);
            var weatherData = JsonConvert.DeserializeObject<WeatherInfo>(jsonString);

            if (weatherData == null || weatherData.Weather == null || weatherData.Weather[0].Description == null || weatherData.Main.Temp == null)
            {
                throw new System.Exception("No valid data retrieved");
            }

            return weatherData;
        }

        private string GetWeatherInfoApi(decimal latitude, decimal longitude)
        {
            string apiKey = System.IO.File.ReadAllText(Server.MapPath("/env/key.txt")); 

            string openWeatherMapApiFormat = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}";
            var client = new WebClient();
            var response = client.DownloadString(string.Format(openWeatherMapApiFormat, latitude, longitude, apiKey));
            return response;

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    public class WeatherInfo
    {
        public List<weather> Weather { get; set; }
        public main Main { get; set; }
        public WeatherInfo()
        {
            this.Weather = new List<weather>();
            this.Main = new main();
        }

        public class main
        {
            public string Temp { get; set; }
        }

        public class weather
        {
            public string Description { get; set; }
        }
    }
}