using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusApp.ViewModel
{
    class JoinVM
    {
        private string phoneNum;

        public string U_PhoneNum
        {
            get { return phoneNum; }
            set { phoneNum = value; }
        }

        private string name;

        public string U_Name
        {
            get { return name; }
            set { name = value; }
        }

        private string birth;

        public string U_Birth
        {
            get { return birth; }
            set { birth = value; }
        }

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
