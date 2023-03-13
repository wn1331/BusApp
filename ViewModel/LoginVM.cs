using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusApp.ViewModel
{
    class LoginVM
    {
        private string id;

        public string U_Id
        {
            get { return id; }
            set { id = value; }
        }

        private string password;

        public string U_Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
