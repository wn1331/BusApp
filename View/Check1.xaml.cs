using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    /// Check1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Check1 : Page
    {
        public Check1()
        {
            InitializeComponent();
            this.DataContext = this;
            idsession.Text = (string)Application.Current.Properties["loginID"]; //아이디세션 받아서 textbox에 띄우기

            //user -> list해서 
            string connectionString1 = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection1 = new MySqlConnection(connectionString1);
            MySqlCommand cmd1 = new MySqlCommand("select * from user", connection1);
            connection1.Open();
            DataTable dt1 = new DataTable();
            dt1.Load(cmd1.ExecuteReader());
            List<User> Users = dt1.DataTableToList<User>();
            connection1.Close();


            //reserveduser -> list화
            string connectionString = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select *from reserveduser", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            List<ReservedUser> reservedUsers = dt.DataTableToList<ReservedUser>();            
            connection.Close();


            List<ReservedUser> reservedusers1 = new List<ReservedUser>();
            string Idsession = Application.Current.Properties["loginID"].ToString();
            User user = Users.Single((x) => x.U_Id.ToString() == (string)Application.Current.Properties["loginID"]);

            foreach (ReservedUser element in reservedUsers)
            {
                if (element.R_PhoneNUm.Equals(user.U_PhoneNum)&& element.R_DepartureDate > DateTime.Now)
                    reservedusers1.Add(element);
            }
            dtGrid.DataContext = reservedusers1;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/view/Home.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/view/Reservation1.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("현재 페이지 입니다.");
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {

            //취소
            //reserveduser에서 예매된 정보 삭제
            string connectionString = @"server=localhost;userid=root;password=5458;database=busapp";
            MySqlConnection connection = null;
            try
            {

                connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "delete from ReservedUser where R_SerialNum='" + this.serialtxt.Text + "';";
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            MessageBox.Show("취소되었습니다.");
            NavigationService.Navigate(new Uri("/view/Reservation1.xaml", UriKind.Relative));



            /////seat정보에서도 삭제---->count도 적용되는거(seat을 list화 해서 사용하니까)/////
            string connectionString2 = @"server=localhost;userid=root;password=5458;database=busapp";
            MySqlConnection connection2 = null;
            try
            {

                connection2 = new MySqlConnection(connectionString2);
                connection2.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection2;
                cmd.CommandText = "delete from Seat where S_Random='" + this.serialtxt.Text + "';";
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

           
        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //셀 선택시 textbox로
            try
            {

                ReservedUser myrow = (ReservedUser)dtGrid.CurrentCell.Item;


                serialtxt.Text = myrow.R_SerialNum.ToString();
                dep.Text = myrow.R_DeparturePlace.ToString();
                arr.Text = myrow.R_ArrivePlace.ToString();
                dept.Text = myrow.R_DepartureTime.ToString();
                arrt.Text = myrow.R_ArriveTime.ToString();
                money_.Text = myrow.R_Money.ToString();
                seatnum.Text = myrow.R_SeatNum.ToString();



            }
            catch (Exception ex)
            {
                MessageBox.Show("예외처리오류");
            }
        }
    }
}
