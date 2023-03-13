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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BusApp.View
{
    /// <summary>
    /// No_Reservation1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class No_Reservation1 : Page
    {
        #region Field

        /// <summary>
        /// 비트맵 이미지 리스트
        /// </summary>
        private List<BitmapImage> bitmapImageList = new List<BitmapImage>();

        /// <summary>
        /// 디스패처 타이머
        /// </summary>
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        /// <summary>
        /// 현재 이미지 스토리보드 시작 여부
        /// </summary>
        private bool startCurrentImageStoryboard = false;

        /// <summary>
        /// 현재 인덱스
        /// </summary>
        private int currentIndex = 0;

        #endregion

        public No_Reservation1()
        {
            InitializeComponent();

            Loaded += Window_Loaded;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
            this.DataContext = this;
        }
        #region 윈도우 로드시 처리하기 - Window_Loaded(sender, e)

        /// <summary>
        /// 윈도우 로드시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.dispatcherTimer.Interval = TimeSpan.FromSeconds(2);

            this.dispatcherTimer.Tick += dispatcherTimer_Tick;

            this.dispatcherTimer.Start();

            SetBitmapImageList();
        }

        #endregion
        #region 디스패처 타이머 틱 발생시 처리하기 - dispatcherTimer_Tick(sender, e)

        /// <summary>
        /// 디스패처 타이머 틱 발생시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.currentIndex++;

            if (this.currentIndex > this.bitmapImageList.Count - 1)
            {
                this.currentIndex = 0;
            }

            if (this.startCurrentImageStoryboard)
            {
                this.currentImage.Source = this.bitmapImageList[this.currentIndex];

                (Resources["CurrentImageStoryboardKey"] as Storyboard).Begin(this);
            }
            else
            {
                this.nextImage.Source = this.bitmapImageList[this.currentIndex];

                (Resources["NextImageStoryboardKey"] as Storyboard).Begin(this);
            }

            this.startCurrentImageStoryboard = !this.startCurrentImageStoryboard;
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////// Function

        #region 리소스 URI 구하기 - GetResourceURI(assemblyName, resourcePath)

        /// <summary>
        /// 리소스 URI 구하기
        /// </summary>
        /// <param name="assemblyName">어셈블리명</param>
        /// <param name="resourcePath">리소스 경로</param>
        /// <returns>리소스 URI</returns>
        private Uri GetResourceURI(string assemblyName, string resourcePath)
        {
            if (string.IsNullOrEmpty(assemblyName))
            {
                return new Uri(string.Format("pack://application:,,,/{0}", resourcePath));
            }
            else
            {
                return new Uri(string.Format("pack://application:,,,/{0};component/{1}", assemblyName, resourcePath));
            }
        }

        #endregion
        #region 비트맵 이미지 리스트 설정하기 - SetBitmapImageList()

        /// <summary>
        /// 비트맵 이미지 리스트 설정하기
        /// </summary>
        private void SetBitmapImageList()
        {
            this.bitmapImageList.Clear();

            string[] resourcePathArray = new string[]
            {
                "이미지/광고1.png",
                "이미지/광고2.png",
                "이미지/광고3.png",
                "이미지/광고4.png",
                "이미지/광고5.png",
            };

            foreach (string resourcePath in resourcePathArray)
            {
                Uri uri = GetResourceURI(null, resourcePath);

                BitmapImage bitmapImage = new BitmapImage(uri);

                this.bitmapImageList.Add(bitmapImage);
            }

            this.currentImage.Source = this.bitmapImageList[this.currentIndex];
        }

        #endregion

        private void Timer_Tick(object sender, EventArgs e)
        {
            Date.Text = DateTime.Now.ToLongDateString();
            Time.Text = DateTime.Now.ToLongTimeString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/view/Home.xaml", UriKind.Relative));

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("현재 페이지 입니다.");
            NavigationService.Navigate(new Uri("/view/No_Reservation1.xaml", UriKind.Relative));

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/view/No_Check1.xaml", UriKind.Relative));
        }




        private void Button_Click_3(object sender, RoutedEventArgs e) //예매하기 버튼
        {
            //모든 값 입력하도록
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

            //nonuser table 에 값 넣기
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost;Database=busapp;UId=root;Password=5458;");
                MySqlCommand comm = new MySqlCommand();
                conn.Open();


                comm.CommandText = "INSERT INTO NONUSER(N_PhoneNum, N_Name) VALUES (@N_PhoneNum, @N_Name)";

                comm.Parameters.Add("@N_PhoneNum", MySqlDbType.Text).Value = textbox1.Text;
                comm.Parameters.Add("@N_Name", MySqlDbType.Text).Value = textbox2.Text;


                comm.Connection = conn;

                comm.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("완료 되었습니다.");
                Application.Current.Properties["non_phone"] = textbox1.Text;
                NavigationService.Navigate(new Uri("/view/No_Reservation2.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show("이미 예매된 전화번호입니다."); }
            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new Uri("/view/Login.xaml", UriKind.Relative));

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/view/Join.xaml", UriKind.Relative));

        }
    }
}
