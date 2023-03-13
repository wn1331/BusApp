using BusApp.Model;
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
    /// No_check1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class No_check1 : Page
    {
        public No_check1()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/view/Home.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("비회원 정보 입력창으로 넘어갑니다.");
            NavigationService.Navigate(new Uri("/view/No_Reservation1.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("현재 페이지 입니다.");
        }

        private void Button3_Click(object sender, RoutedEventArgs e)  //취소버튼
        {
            //취소
            //reservednonuser에서 삭제
            string connectionString = @"server=localhost;userid=root;password=5458;database=busapp";
            MySqlConnection connection = null;
            try
            {

                connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "delete from ReservedNonUser where RN_SerialNum='" + this.serialtxt.Text + "';";
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }


            MessageBox.Show("취소되었습니다.");
            NavigationService.Navigate(new Uri("/view/No_Reservation1.xaml", UriKind.Relative));



            /////seat정보에서도 삭제//////
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

        private void Button_Click_3(object sender, RoutedEventArgs e) //전화번호 조회
        {

            //reservednonuser -> list
            string connectionString = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from reservednonuser", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            List<ReservedNonUser> RE_NoNUsers = dt.DataTableToList<ReservedNonUser>();

            
            connection.Close();
            List<ReservedNonUser> RE_NoNUsers1 = new List<ReservedNonUser>();

            //전화번호 비교해서 가져오기
            string ex1 = searchh.Text;
            foreach (ReservedNonUser element in RE_NoNUsers)
            {
                if (element.RN_PhoneNum.Equals(ex1) && element.RN_DepartureDate > DateTime.Now)
                    RE_NoNUsers1.Add(element);
            }

            dtGrid.DataContext = RE_NoNUsers1;
        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //셀 선택한 값 textbox에 뜨도록
            try
            {
                
                ReservedNonUser myrow = (ReservedNonUser)dtGrid.CurrentCell.Item;

                serialtxt.Text = myrow.RN_SerialNum.ToString();
                dep.Text = myrow.RN_DeparturePlace.ToString();
                arr.Text = myrow.RN_ArrivePlace.ToString();
                dept.Text = myrow.RN_DepartureTime.ToString();
                arrt.Text = myrow.RN_ArriveTime.ToString();
                money_.Text = myrow.RN_Money.ToString();
                seatnum.Text = myrow.RN_SeatNum.ToString();
                //Application.Current.Properties["BusNumsession"] = myrow.B_BusNum;

                //여기에 Mysql 삭제문

            }
            catch (Exception ex)
            {
                MessageBox.Show("예외처리오류");
            }
        }
    }
}
