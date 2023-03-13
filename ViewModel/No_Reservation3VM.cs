using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusApp.ViewModel
{
    class No_Reservation3VM
    {
        private int serialNum;

        public int RN_SerialNum
        {
            get { return serialNum; }
            set { serialNum = value; }
        }

        private string phoneNum;

        public string RN_PhoneNum
        {
            get { return phoneNum; }
            set { phoneNum = value; }
        }

        private string name;

        public string RN_Name
        {
            get { return name; }
            set { name = value; }
        }

        private DateTime departureDate;

        public DateTime RN_DepartureDate
        {
            get { return departureDate; }
            set { departureDate = value; }
        }




        private string departurePlace;

        public string RN_DeparturePlace
        {
            get { return departurePlace; }
            set { departurePlace = value; }
        }



        private string arrivePlace;

        public string RN_ArrivePlace
        {
            get { return arrivePlace; }
            set { arrivePlace = value; }
        }

        private int seatNum;

        public int RN_SeatNum
        {
            get { return seatNum; }
            set { seatNum = value; }
        }

        private int money;

        public int RN_Money
        {
            get { return money; }
            set { money = value; }
        }

        private int departureTime;

        public int RN_DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }

        private int arriveTime;

        public int RN_ArriveTime
        {
            get { return arriveTime; }
            set { arriveTime = value; }
        }

        private string busNum;

        public string RN_BusNum
        {
            get { return busNum; }
            set { busNum = value; }
        }
    }
}
