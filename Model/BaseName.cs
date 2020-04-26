using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public abstract class BaseName
    {

        public BaseName()
        {
            PROFILE_PIC = string.Empty;
            UNIQUE_ID = string.Empty;
            FIRST_NAME = string.Empty;
            MIDDLE_NAME = string.Empty;
            LAST_NAME = string.Empty;
            DOB = DateTime.Now.ToString("yyyy-MM-dd");
            AGE = "0";
            COURSE = string.Empty;
            SECTION = string.Empty;
            ADDRESS = string.Empty;
            CONTACT_NO = string.Empty;
            DATE_ADDED = DateTime.Now.ToString("yyyy-MM-dd");
            ADDED_BY = string.Empty;
            MODIFIED_DATE = DateTime.Now.ToString("yyyy-MM-dd");                        
            MODIFIED_BY = string.Empty;
            STATUS = string.Empty;
            
        }

        public string PROFILE_PIC { get; set; }
        public string UNIQUE_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string DOB { get; set; }
        public string AGE { get; set; }
        public string COURSE { get; set; }
        public string SECTION { get; set; }
        public string ADDRESS { get; set; }
        public string CONTACT_NO { get; set; }
        public string DATE_ADDED { get; set; }
        public string ADDED_BY { get; set; }
        public string MODIFIED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public string STATUS { get; set; }     

    }
}
