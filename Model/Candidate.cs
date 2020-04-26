using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Candidate : BaseName
    {
        public Candidate()
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

            ELECTION_CODE = string.Empty;
            _Position = new Position();
            _Party = new Party();                      
        }

        public string ELECTION_CODE { get; set; }          
        public Position _Position { get; set; }
        public Party _Party { get; set; }                        
  
        
    }
}
