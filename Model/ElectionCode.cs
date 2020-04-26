using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ElectionCode
    {
        public ElectionCode()
        {
            CODE = string.Empty;
            STATUS = string.Empty;
            ADDED_DATE = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            ADDED_BY = string.Empty;
        }
        public string CODE { get; set; }
        public string STATUS { get; set; }
        public DateTime ADDED_DATE { get; set; }
        public string ADDED_BY { get; set; }        
    }
}
