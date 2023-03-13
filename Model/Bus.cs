using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusApp
{
    class Bus : Notifier
    {
        private string busNum;

        public string B_BusNum
        {
            get { return busNum; }
            set { busNum = value; }
        }


        private int totalSeat;

        public int B_TotalSeat
        {
            get { return totalSeat; }
            set { totalSeat = value; }
        }

        private string departurePlace;

        public string B_DeparturePlace
        {
            get { return departurePlace; }
            set { departurePlace = value; }
        }

        private string arrivePlace;

        public string B_ArrivePlace
        {
            get { return arrivePlace; }
            set { arrivePlace = value; }
        }


        private int money;

        public int B_Money
        {
            get { return money; }
            set { money = value; }
        }


        private int departureTime;

        public int B_DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }


        private int arriveTime;

        public int B_ArriveTime
        {
            get { return arriveTime; }
            set { arriveTime = value; }
        }


    }
}
