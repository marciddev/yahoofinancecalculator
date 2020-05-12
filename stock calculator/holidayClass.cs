using System;
using System.Collections.Generic;
using System.Text;

namespace stock_calculator
{
    public class holidayClass
    {
        public int status { get; set; }
        public String warning { get; set; }
        public Request requests { get; set; }
        public List<Holiday> holidays { get; set; }
    }

    public class Request
    {
        public int used { get; set; }
        public int available { get; set; }
        public String resets { get; set; }
    }

    public class Holiday
    {
        public String name { get; set; }
        public String date { get; set; }
        public String observed { get; set; }
        public String country { get; set; }
        public String uuid { get; set; }
        public Boolean @public {get;set;}
        public Weekday weekday { get; set; }
    }

    public class WeekdayContent
    {
        public String name { get; set; }
        public String numeric { get; set; }
    }
    public class Weekday
    {
        public WeekdayContent date { get; set; }
        public WeekdayContent observed { get; set; }
    }
}
