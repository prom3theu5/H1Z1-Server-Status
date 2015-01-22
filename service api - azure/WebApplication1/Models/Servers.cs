using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h1z1status.Models
{
    public class Servers
    {
        public Servers()
        {
            US = new List<Server>();
            EU = new List<Server>();
        }
        
        public List<Server> US { get; set; }
        public List<Server> EU { get; set; }
    }
}