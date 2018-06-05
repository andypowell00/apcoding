using System;
using System.Collections.Generic;

namespace apapi.Models
{
    public class Weather
    {
        public string city { get; set; }
        public string id { get; set; }
        public decimal lat {get; set;}
        public decimal lng {get;set;}
        public string status {get; set;}
        public string description {get;set ;}
        public string temp {get;set;}
        public string temp_min {get;set;}
        public string temp_max {get;set;}
        public string windspeed {get;set;}
        public string humidity {get;set;}

    }
}