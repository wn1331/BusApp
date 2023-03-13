using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Login : Page
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

        public Login()
        {
            InitializeComponent();

            Loaded += Window_Loaded;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
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

        private void Button2_Click(object sender, RoutedEventArgs e) //가입하기로
        {
            NavigationService.Navigate(new Uri("/view/Join.xaml", UriKind.Relative));
        }

        private void Button1_Click(object sender, RoutedEventArgs e) //홈으로
        {
            NavigationService.Navigate(new Uri("/view/Home.xaml", UriKind.Relative));
        }

        private void Button4_Click(object sender, RoutedEventArgs e) //로그인버튼
        {
            //id password 모두 입력할 시 로그인 되게
            if (string.IsNullOrEmpty(textbox1.Text))
            {
                MessageBox.Show("Id를 입력해주세요");
                this.textbox1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(passwordbox.Password))
            {
                MessageBox.Show("password를 입력해주세요");
                this.passwordbox.Focus();
                return;
            }

            string U_Id = textbox1.Text;
            string U_Password = passwordbox.Password;


            //user -> list화해서 입력한 아이디와 비밀번호와 일치하는지 
            string connectionString = "Server=localhost;Database=busapp;UId=root;Password=5458;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from user", connection);
            connection.Open();
            DataTable dt = new DataTable();

            dt.Load(cmd.ExecuteReader());

            List<User> Users = dt.DataTableToList<User>();
            try
            {
                User user1 = Users.Single((x) => x.U_Id.Equals(U_Id));


                if (user1.U_Password.Equals(U_Password))
                {
                    Application.Current.Properties["loginID"] = user1.U_Id;
                    MessageBox.Show("로그인 되었습니다.");
                    NavigationService.Navigate(new Uri("/view/Reservation1.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("비밀번호가 틀렸습니다.");
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("존재하지 않는 아이디입니다.");
            }






        }

        private void Button_Click(object sender, RoutedEventArgs e) 
        {
            MessageBox.Show("비회원 예약 확인으로 넘어갑니다.");
            NavigationService.Navigate(new Uri("/view/No_Check1.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("비회원 예매로 넘어갑니다.");
            NavigationService.Navigate(new Uri("/view/No_Reservation1.xaml", UriKind.Relative));
        }

    }
}
