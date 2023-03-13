using BusApp.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BusApp.View
{
    /// <summary>
    /// Reservation2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Reservation2 : Page
    {
        
        public Reservation2()
        {
            InitializeComponent();
            this.DataContext = this;
            
                     

            //user table -> list 
            string connectionString = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from user", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            List<User> Users = dt.DataTableToList<User>();

            //id session으로 리스트에서 찾아서 text box에 넣기
            User user = Users.Single((x) => x.U_Id.ToString() == (string)Application.Current.Properties["loginID"]);
            birth1.Text = user.U_Birth.ToString();
            phone2.Text = user.U_PhoneNum.ToString();

            connection.Close();

            //bus -> list 
            string connectionString1 = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);
            MySqlCommand cmd1 = new MySqlCommand("select * from bus", connection1);
            connection1.Open();
            DataTable dt1 = new DataTable();
            dt1.Load(cmd1.ExecuteReader());
            List<Bus> Buses = dt1.DataTableToList<Bus>();


            //busnum session으로 리스트에서 찾아서 textbox에 넣기
            Bus bus = Buses.Single((x) => x.B_BusNum.ToString() == (string)Application.Current.Properties["BusNumsession"]);
            busnum3.Text = bus.B_BusNum.ToString();
            ddate4.Text = Application.Current.Properties["DataSession"].ToString();
            dplace5.Text = bus.B_DeparturePlace.ToString();
            aplace6.Text = bus.B_ArrivePlace.ToString();
            dtime7.Text = bus.B_DepartureTime.ToString();
            atime8.Text = bus.B_ArriveTime.ToString();
            connection.Close();
            //birth->age로 바꾸고 money계산
            double d;
            int a = int.Parse(user.U_Birth);
            int b = (int.Parse(DateTime.Now.ToString("yyyymmdd")));
            int age = (b - a) / 10000 + 1;
            if (age >= 0 && age <= 8) d = 0.8;
            else if (age >= 9 && age <= 13) d = 0.9;
            else d = 1;

            money10.Text = ((bus.B_Money) * d).ToString();

            connection.Close();

          

        }


        private void Button23_Click(object sender, RoutedEventArgs e) //결제버튼 

        {
            //고유키로 줄 랜덤 번호 생성(예약자의 랜덤번호가 해당 seat 테이블의 랜덤번호 )
            int r2 = new Random().Next(1000);
            int random = r2;



            if (string.IsNullOrEmpty(birth1.Text))
            {
                MessageBox.Show("생년월일을 입력해주세요");
                this.birth1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(phone2.Text))
            {
                MessageBox.Show("전화번호를 입력해주세요");
                this.phone2.Focus();
                return;
            }
            if (string.IsNullOrEmpty(busnum3.Text))
            {
                MessageBox.Show("버스번호를 입력해주세요");
                this.busnum3.Focus();
                return;
            }
            if (string.IsNullOrEmpty(ddate4.Text))
            {
                MessageBox.Show("출발날짜를 입력해주세요");
                this.ddate4.Focus();
                return;
            }
            if (string.IsNullOrEmpty(dplace5.Text))
            {
                MessageBox.Show("출발장소를 입력해주세요");
                this.dplace5.Focus();
                return;
            }
            if (string.IsNullOrEmpty(aplace6.Text))
            {
                MessageBox.Show("도착장소를 입력해주세요");
                this.aplace6.Focus();
                return;
            }
            if (string.IsNullOrEmpty(dtime7.Text))
            {
                MessageBox.Show("출발시간을 입력해주세요");
                this.dtime7.Focus();
                return;
            }
            if (string.IsNullOrEmpty(atime8.Text))
            {
                MessageBox.Show("도착시간을 입력해주세요");
                this.atime8.Focus();
                return;
            }
            if (string.IsNullOrEmpty(seatNum.Text))
            {
                MessageBox.Show("좌석번호를 선택해주세요");
                this.seatNum.Focus();
                return;
            }
            if (string.IsNullOrEmpty(money10.Text))
            {
                MessageBox.Show("결제금액을 입력해주세요");
                this.money10.Focus();
                return;
            }



            ////////좌석테이블 생성///////


            try
            {


                MySqlConnection conn = new MySqlConnection("Server=localhost;Database=busapp;UId=root;Password=5458;");
                MySqlCommand comm = new MySqlCommand();
                conn.Open();


                comm.CommandText = "INSERT INTO Seat(S_random, S_BusNum, S_DepartureDate) VALUES (@S_random, @S_BusNum, @S_DepartureDate)";

                comm.Parameters.Add("@S_random", MySqlDbType.Text).Value = random;
                comm.Parameters.Add("@S_BusNum", MySqlDbType.Text).Value = busnum3.Text;
                comm.Parameters.Add("@S_DepartureDate", MySqlDbType.Text).Value = ddate4.Text;


                comm.Connection = conn;

                comm.ExecuteNonQuery();
                conn.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("seat error.");
            }




            ///////////////////좌석테이블 수정(랜덤 값 부여하기)//////////////////////
            

            string connectionString5 = @"server=localhost;userid=root;password=5458;database=busapp";
            MySqlConnection connection = null;
            try
            {

                connection = new MySqlConnection(connectionString5);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = "UPDATE seat SET " + this.seatNum.Text + " = 0 WHERE S_Random = " + random + "; ";

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }





            /////////////Reserveduser생성////////////////////
          
            
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost;Database=busapp;UId=root;Password=5458;");
                MySqlCommand comm = new MySqlCommand();
                conn.Open();

                comm.CommandText = "INSERT INTO ReservedUser(R_SerialNum,R_PhoneNum, R_Birth, R_DepartureDate, R_Money, R_DeparturePlace, R_ArrivePlace, R_SeatNum, R_DepartureTime, R_ArriveTime, R_BusNum ) " +
                    "VALUES (@R_SerialNum,@R_PhoneNum, @R_Birth, @R_DepartureDate, @R_Money, @R_DeparturePlace, @R_ArrivePlace ,@R_SeatNum, @R_DepartureTime, @R_ArriveTime, @R_BusNum)";

                comm.Parameters.Add("@R_SerialNum", MySqlDbType.Text).Value = random;
                comm.Parameters.Add("@R_PhoneNum", MySqlDbType.Text).Value = phone2.Text;
                comm.Parameters.Add("@R_Birth", MySqlDbType.Text).Value = birth1.Text;
                comm.Parameters.Add("@R_DepartureDate", MySqlDbType.Text).Value = ddate4.Text;
                comm.Parameters.Add("@R_Money", MySqlDbType.Text).Value = money10.Text;
                comm.Parameters.Add("@R_DeparturePlace", MySqlDbType.Text).Value = dplace5.Text;
                comm.Parameters.Add("@R_ArrivePlace", MySqlDbType.Text).Value = aplace6.Text;
                comm.Parameters.Add("@R_SeatNum", MySqlDbType.Text).Value = seatNum.Text;
                comm.Parameters.Add("@R_DepartureTime", MySqlDbType.Text).Value = dtime7.Text;
                comm.Parameters.Add("@R_ArriveTime", MySqlDbType.Text).Value = atime8.Text;
                comm.Parameters.Add("@R_BusNum", MySqlDbType.Text).Value = busnum3.Text;


                comm.Connection = conn;

                comm.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("결제 되었습니다.");
                NavigationService.Navigate(new Uri("/view/Reservation1.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show("결제예외 오류"); }
            


            
           

            

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            NavigationService.Navigate(new Uri("/view/Home.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("이전 페이지로 돌아갑니다.");
            NavigationService.Navigate(new Uri("/view/Reservation1.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/view/Check1.xaml", UriKind.Relative));
        }





        //좌석 선택시 textbox에 번호 불러오기 && 좌석 선택된 여부

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_1.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 선택된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_1";  }


                  
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_2.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_2"; }
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_3.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_3"; }
        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_4.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_4"; }
        }

        private void radioButton4_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_5.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_5"; }
        }

        private void radioButton5_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_6.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_6"; }
        }

        private void radioButton6_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_7.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_7"; }
        }

        private void radioButton7_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_8.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_8"; }
        }

        private void radioButton8_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_9.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_9"; }
        }

        private void radioButton9_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_10.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_10"; }
        }

        private void radioButton10_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_11.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_11"; }
        }

        private void radioButton11_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_12.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_12"; }
        }

        private void radioButton12_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_13.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_13"; }
        }

        private void radioButton13_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_14.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_14"; }
        }


        private void radioButton14_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_15.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_15"; }
        }

        private void radioButton15_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_16.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_16"; }
        }

        private void radioButton16_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_17.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_17"; }
        }

        private void radioButton17_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_18.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_18"; }
        }

        private void radioButton18_Checked(object sender, RoutedEventArgs e)
        {
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();


            bool exist1 = false;
            bool exist = seats.Exists((x) => x.S_BusNum == busnum3.Text && x.S_DepartureDate == ddate4.Text && x.S_19.ToString() == exist1.ToString());
            if (exist == true) { MessageBox.Show("이미 예매된 좌석입니다. 다른 좌석을 선택해주세요"); }
            else { seatNum.Text = "S_19"; }
        }



        
    }
}
