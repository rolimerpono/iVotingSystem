using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class VotedCandidate
    {
        public VotedCandidate()
        {
            CANDIDATE_ID = string.Empty;
            FIRST_NAME = string.Empty;
            MIDDLE_NAME = string.Empty;
            LAST_NAME = string.Empty;
            DOB = DateTime.Now;
            AGE = String.Empty;
            COURSE = string.Empty;
            SECTION = string.Empty;
            ADDRESS = string.Empty;
            CONTACT_NO = string.Empty;

            POSITION_ID = string.Empty;
            POSITION = string.Empty;
            PARTY_ID = string.Empty;
            PARTY = string.Empty;

            VOTERS_ID = string.Empty;

            PROFILE_PIC = string.Empty;

            ELECTION_CODE = string.Empty;
        }

        public string CANDIDATE_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public DateTime DOB { get; set; }
        public string AGE { get; set; }
        public string COURSE { get; set; }
        public string SECTION { get; set; }
        public string ADDRESS { get; set; }
        public string CONTACT_NO { get; set; }
        public DateTime DATE_ADDED { get; set; }
        public DateTime DATE_MODIFIED { get; set; }

        public string POSITION_ID { get; set; }
        public string POSITION { get; set; }
        public string PARTY_ID { get; set; }
        public string PARTY { get; set; }

        public string VOTERS_ID { get; set; }
        public string PROFILE_PIC { get; set; }
        public string ELECTION_CODE { get; set; }
    
    }
}
