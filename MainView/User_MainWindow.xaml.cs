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

namespace User_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class User_MainWindow : Window
    {
        public User_MainWindow()
        {
            InitializeComponent();
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null) return;

            switch (radioButton.Content.ToString())
            {
                case "Move - Delivery":
                _mainFrame.Navigate(new Page2());
                break;

                case "Home":
                _mainFrame.Navigate(new User_MainWindow());
                break;
                
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Class1.TripInformation tripInformation = new Class1.TripInformation();
            var tripInfo = tripInformation.TripInfor; // Sử dụng thuộc tính TripInfor
            if (tripInfo != "None Information")
            {
                // Nếu có thông tin chuyến đi, hiển thị thông tin trong TextBox
                TripInforTextBlock.Text="Ban co mot chuyen di sap den";
            }
            else
            {
                // Nếu không có thông tin chuyến đi, hiển thị thông báo trong TextBox
                TripInforTextBlock.Text="Khong co thong tin chuyen di";
            }
        }


        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Kiểm tra xem có thông tin chuyến đi không
            Class1.TripInformation tripInformation = new Class1.TripInformation();
            var tripInfo = tripInformation.TripInfor; //thuoc tinh TripInfor
            if (tripInfo != "None Information")
            {
                //Dieu huong den trang thong tin
                _mainFrame.Navigate(new Page_TripInfor());
            }
        }
    }
}
