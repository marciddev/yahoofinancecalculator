using System;
using System.Collections.Generic;
using System.Text;

namespace stock_calculator
{
    public class apiStuff
    {
        public String symbol { get; set; }
        public List<historicalObject> historical { get; set; }
    }
    public class historicalObject
    {
        public String date { get; set; }
        public String close { get; set; }
    }
}
