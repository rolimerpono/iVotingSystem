using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Position
    {

        public Position()
        {
            this.ID = string.Empty;
            this.POSITION = string.Empty;
            this.STATUS = string.Empty;
        }

        public string ID { get; set; }
        public string POSITION { get; set; }
        public string STATUS { get; set; }


    }
}
