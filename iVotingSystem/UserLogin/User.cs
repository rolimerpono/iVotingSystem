using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonFunction;

namespace iVotingSystem.DefaultLogin
{
    public class User
    {

        CommonFunction.CommonFunction oUtility  = new CommonFunction.CommonFunction();

        public User()
        {
            USERNAME =oUtility.Encrypt("admin");
            PASSWORD = oUtility.Encrypt("rolly");
            FULLNAME =oUtility.Encrypt("Rolimer Pono");
            ROLE = oUtility.Encrypt("System Administrator");
        }

        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string FULLNAME { get; set; }
        public string ROLE { get; set; }
        

    }
}
