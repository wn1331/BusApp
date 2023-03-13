using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusApp
{
    class ReservedUser
    {
        private int serialNum; 

        public int R_SerialNum
        {
            get { return serialNum; }
            set { serialNum = value; }
        }

        private string phoneNum;

        public string R_PhoneNUm
        {
            get { return phoneNum; }
            set { phoneNum = value; }
        }

        private string birth;

        public string R_Birth
        {
            get { return birth; }
            set { birth = value; }
        }

        private DateTime departureDate;

        public DateTime R_DepartureDate
        {
            get { return departureDate; }
            set { departureDate = value; }
        }

      

        private int money;

        public int R_Money
        {
            get { return money; }
            set { money = value; }
        }

        private string departurePlace;

        public string R_DeparturePlace
        {
            get { return departurePlace; }
            set { departurePlace = value; }
        }

        private string arrivePlace;

        public string R_ArrivePlace
        {
            get { return arrivePlace; }
            set { arrivePlace = value; }
        }

        private string seatNum;

        public string R_SeatNum
        {
            get { return seatNum; }
            set { seatNum = value; }
        }

        private int departureTime;

        public int R_DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }


        private int arriveTime;

        public int R_ArriveTime
        {
            get { return arriveTime; }
            set { arriveTime = value; }
        }


        private string busBum;

        public string R_BusNum
        {
            get { return busBum; }
            set { busBum = value; }
        }




    }
}
