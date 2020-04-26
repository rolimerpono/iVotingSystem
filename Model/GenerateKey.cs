using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class GenerateKey
    {

        public GenerateKey()
        {
            ID  = string.Empty;
            VOTERS_ID  = string.Empty;
            GENERATED_KEY = string.Empty;
            DATED_ADDED = string.Empty;
            STATUS = string.Empty;
            ELECTION_CODE = string.Empty;
            
        }

        public string ID { get; set; }
        public string VOTERS_ID { get; set; }
        public string GENERATED_KEY {get;set;}
        public string DATED_ADDED {get;set;}
        public string STATUS  {get;set;}
        public string ELECTION_CODE { get; set; }
    }
}
