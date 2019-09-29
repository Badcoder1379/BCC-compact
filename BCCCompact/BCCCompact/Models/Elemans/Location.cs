using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class Location
    {
        public double x { get; set; }
        public double y { get; set; }
        public Location(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}