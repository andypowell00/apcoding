using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using apapi.Models;
using Microsoft.AspNetCore.Cors;

namespace apapi.Controllers
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private readonly string tmbaseurl;
        private readonly string tmapikey;
        public EventsController(IOptions<Settings> settings){
            tmbaseurl = settings.Value.TMBaseUrl;
            tmapikey = settings.Value.TMApiKey;
        }
        // GET api/events
        [HttpGet]
        public string Get()
        {
             WebClient client = new WebClient ();
             string reply = client.DownloadString(tmbaseurl + "discovery/v2/events.json?apikey=" + tmapikey);  
             
             return reply;
        }

        // GET api/events/QS
        [HttpGet("{qs}")]
        public string Get(string qs)
        {
            WebClient client = new WebClient ();
            string reply = client.DownloadString(tmbaseurl + "discovery/v2/events.json?" + qs + "&apikey=" + tmapikey);
            dynamic jsresp = JsonConvert.DeserializeObject(reply);
            List<Event> listOfEvents = new List<Event>();
             foreach(var item in jsresp._embedded.events){
                 listOfEvents.Add(new Event() {
                 name = item.name,
                 url = item.url,
                 type = item.type,
                 id = item.id
                }); 
             }
            return JsonConvert.SerializeObject(listOfEvents);
        }
       
           // GET api/events/keyword
        [Route("GetClassByKw")]
        public string GetClassByKw([FromQuery] string kw)
        {
            WebClient client = new WebClient ();
            string reply = client.DownloadString(tmbaseurl + "discovery/v2/classifications.json?keyword=" +kw + "&apikey=" + tmapikey);
            return reply;
        }
        [Route("GetAttractionByKw")]
        public string GetAttractionByKw([FromQuery] string kw)
        {
            WebClient client = new WebClient ();
            string reply = client.DownloadString(tmbaseurl + "discovery/v2/attractions.json?keyword=" +kw + "&apikey=" + tmapikey);
            return reply;
        }
     
        
    }
}
