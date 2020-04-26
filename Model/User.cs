using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class User
    {

        public User()
        {
            USERNAME = string.Empty;
            FULLNAME = string.Empty;
            PASSWORD = string.Empty;
            ROLE = string.Empty;
            CONTACT_NO = string.Empty;
            ADDRESS = string.Empty;
        }
        public string USERNAME { get; set; }
        public string FULLNAME { get; set; }
        public string PASSWORD { get; set; }
        public string ROLE { get; set; }
        public string CONTACT_NO { get; set; }
        public string ADDRESS { get; set; }
    }
}
