using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using apapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace apapi.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        
        private readonly string owmbaseurl;
        private readonly string owmapikey;
        public WeatherController(IOptions<Settings> settings){
            owmbaseurl = settings.Value.OWMBaseUrl;
            owmapikey = settings.Value.OWMApiKey;
        }
        
        // GET api/weather 

        [HttpGet]
        public string Get()
        {
            return "ello";
        }

        // GET api/weather/5 (grab single city's current weather based on ID)
        [HttpGet("{id}")] 
        public string Get(int id)
        {
            WebClient client = new WebClient ();
            string reply = client.DownloadString(owmbaseurl + "weather?id=" + id + "&units=imperial&appid=" + owmapikey);
            dynamic jsresp = JsonConvert.DeserializeObject(reply);
            Weather citycurrent = new Weather(){
                lat = jsresp.coord["lat"],
                lng = jsresp.coord["lon"],
                city = jsresp.name,
                id = jsresp.id,
                windspeed = jsresp.wind["speed"],
                temp = jsresp.main["temp"],
                temp_max = jsresp.main["temp_max"],
                temp_min = jsresp.main["temp_min"],
                humidity = jsresp.main["humidity"],
                description = jsresp.weather["description"],
                status = jsresp.weather["main"]
            };
            return JsonConvert.SerializeObject(citycurrent);
        }

        // POST api/weather
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/weather/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/weather/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
