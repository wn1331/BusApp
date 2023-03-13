using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    /// Join.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Join : Page
    {
        public Join()
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
            MessageBox.Show("비회원 예매로 넘어갑니다.");
            NavigationService.Navigate(new Uri("/view/No_Reservation1.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("비회원 예약 확인으로 넘어갑니다.");
            NavigationService.Navigate(new Uri("/view/No_Check1.xaml", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //가입하기
        {
            //모든정보입력하도록
            if (string.IsNullOrEmpty(textbox1.Text))
            {
                MessageBox.Show("전화번호를 입력해주세요");
                this.textbox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textbox2.Text))
            {
                MessageBox.Show("이름을 입력해주세요");
                this.textbox2.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textbox3.Text))
            {
                MessageBox.Show("생년월일을 입력해주세요");
                this.textbox3.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textbox4.Text))
            {
                MessageBox.Show("Id를 입력해주세요");
                this.textbox4.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textbox5.Text))
            {
                MessageBox.Show("password를 입력해주세요");
                this.textbox5.Focus();
                return;
            }



            //db에 textbox내용으로 usertable만들기
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost;Database=busapp;UId=root;Password=5458;");
                MySqlCommand comm = new MySqlCommand();
                conn.Open();


                comm.CommandText = "INSERT INTO USER(U_PhoneNum, U_Name, U_Birth, U_Id, U_Password) VALUES (@U_PhoneNum, @U_Name, @U_Birth, @U_Id, @U_Password)";

                comm.Parameters.Add("@U_PhoneNum", MySqlDbType.Text).Value = textbox1.Text;
                comm.Parameters.Add("@U_Name", MySqlDbType.Text).Value = textbox2.Text;
                comm.Parameters.Add("@U_Birth", MySqlDbType.Text).Value = textbox3.Text;
                comm.Parameters.Add("@U_Id", MySqlDbType.Text).Value = textbox4.Text;
                comm.Parameters.Add("@U_Password", MySqlDbType.Text).Value = textbox5.Text;

                comm.Connection = conn;

                comm.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("회원가입 되었습니다.");
                NavigationService.Navigate(new Uri("/view/Login.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show("전화번호나 아이디가 중복 되었습니다."); }
        }
    }
}
