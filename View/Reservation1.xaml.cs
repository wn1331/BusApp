using MySql.Data.MySqlClient;
using System;
using BusApp.Model;
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
    /// Reservation1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Reservation1 : Page
    {
        public Reservation1()
        {
            InitializeComponent();
            this.DataContext = this;

        }
        private void Button4_Click(object sender, RoutedEventArgs e) //예매하기 버튼
        {

            //count값 구하기//
            string co = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection10 = new MySqlConnection(co);
            MySqlCommand cmd10 = new MySqlCommand("select * from seat", connection10);
            connection10.Open();
            DataTable dt10 = new DataTable();
            dt10.Load(cmd10.ExecuteReader());
            List<Seat> seats = dt10.DataTableToList<Seat>();



            int count1 = seats.Count((x) => x.S_BusNum == bnum.Text && x.S_DepartureDate == txtSelectedDate.Text);
            int count = 19 - count1;

            
            //매진일시 재선택 매진 아닐시 다음페이지로
            if (count <= 0)
            {
                MessageBox.Show("이 버스는 이미 매진되었습니다. 다른 버스를 선택해주세요");

            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(txtSelectedDate.Text))
                    {
                        MessageBox.Show("달력에서 날짜를 선택해 주세요");
                        this.txtSelectedDate.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(dep.Text))
                    {
                        MessageBox.Show("버스를 선택해주세요");
                        this.dep.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(arr.Text))
                    {
                        MessageBox.Show("버스를 선택해주세요");
                        this.arr.Focus();
                        return;
                    }



                    Application.Current.Properties["DataSession"] = txtSelectedDate.Text;       //날짜 정보 string화 해서 세션으로 넘기기
                    MessageBox.Show(count+"개의 좌석이 남았습니다.");
                    MessageBox.Show("\t0-8세는 20% 할인 운행, \n 9-13세는 10% 할인 운행이 적용 됩니다.");
                    NavigationService.Navigate(new Uri("/view/Reservation2.xaml", UriKind.Relative));
                }
                catch (Exception ex) 
                { MessageBox.Show("예매오류"); }

            }
            

            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new Uri("/view/Home.xaml", UriKind.Relative));

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("현재 페이지 입니다.");
            NavigationService.Navigate(new Uri("/view/Reservation1.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/view/Check1.xaml", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)  //조회하기 버튼
        {
            //bus->list화
            string connectionString = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from bus", connection);
            connection.Open();
            DataTable dt = new DataTable();

            dt.Load(cmd.ExecuteReader()); 

            List<Bus> Buses = dt.DataTableToList<Bus>();

            string ex1 = region1.Text;
            string ex2 = region2.Text;

            List<Bus> Buses1 = new List<Bus>();
            List<Bus> Buses2 = new List<Bus>();

            //각 행선지에 맞는 버스가 데이터 그리드 뷰에 뜨게
            foreach (Bus element in Buses)
            {
                if (element.B_DeparturePlace.Equals(ex1))
                    Buses1.Add(element);
            }
            foreach (Bus element in Buses1)
            {
                if (element.B_ArrivePlace.Equals(ex2))
                    Buses2.Add(element);
            }

            dtGrid.DataContext = Buses2;
            connection.Close();

        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //셀 선택한 값 textbox에 넣기
           
            try
                {

                    Bus myrow = (Bus)dtGrid.CurrentCell.Item;
                    
                    dep.Text = myrow.B_DepartureTime.ToString();
                    arr.Text = myrow.B_ArriveTime.ToString();
                    bnum.Text = myrow.B_BusNum.ToString();
                    
                Application.Current.Properties["BusNumsession"] = myrow.B_BusNum;


            }
            catch (Exception ex)
                {
                    MessageBox.Show("예외처리오류");
                }
            
        }

      
    }
}
