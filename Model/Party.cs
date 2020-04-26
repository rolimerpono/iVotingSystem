using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Party
    {

        public Party()
        {
            this.ID = string.Empty;
            this.PARTY = string.Empty;
            this.STATUS = string.Empty;
        }

        public string ID { get; set; }
        public string PARTY { get; set; }
        public string STATUS { get; set; }
    }
}
