using System;
using System.Collections.Generic;

namespace apapi.Models
{
    public class Event
    {
     
       
        public string name { get; set; }
        public string id { get; set; }
        public string url { get; set; } = string.Empty;
        public string type {get;set;}
    }
}