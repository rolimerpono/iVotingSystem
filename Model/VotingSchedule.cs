using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class VotingSchedule
    {
        public VotingSchedule()
        {
            DATE_START = DateTime.Now.ToString("yyyy-MM-dd");
            DATE_END = DateTime.Now.ToString("yyyy-MM-dd");
            TIME_START = DateTime.Now.ToString("hh:mm tt");
            TIME_END = DateTime.Now.ToString("hh:mm tt");
            STATUS = "OPEN";
        }
        public string DATE_START { get; set; }
        public string DATE_END { get; set; }
        public string TIME_START { get; set; }
        public string TIME_END { get; set; }
        public string STATUS { get; set; }

    }
}
