using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apapi
{
    public class Settings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string TMBaseUrl { get; set; }
        public string TMApiKey { get; set; }
        public string OWMApiKey { get; set; }
        public string OWMBaseUrl { get; set; }
    }
}
