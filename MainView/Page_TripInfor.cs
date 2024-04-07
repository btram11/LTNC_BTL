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
    /// Interaction logic for Page_TripInfor.xaml
    /// </summary>
    public partial class Page_TripInfor : Page
    {
        public Page_TripInfor()
        {
            InitializeComponent();
        }

        /*
        public class TripInformation
        {
            public TripInformation()
            {
                Type = "None Information";
                TripInfor = "None Information";
                CostEstimate = "0";
                DriverPhone = "None Information";
                DriverName = "None Information";
            }
            public string Type { get; set; }
            public string TripInfor { get; set; } //thong tin ve duong di
            public string CostEstimate { get; set; }
            public string DriverPhone { get; set; }
            public string DriverName { get; set; }

        }
        */


        private List<Class1.TripInformation> LoadDataFromDatabase()
        {
            //truy van CSDL lay thong tin chuyen di duoc chon
            throw new NotImplementedException();

        }


        private void YourPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Tải dữ liệu từ cơ sở dữ liệu

            var data = LoadDataFromDatabase();

            // Cập nhật giao diện người dùng
            foreach (var item in data)
            {
                var card = new Border
                {
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(5),
                    Padding = new Thickness(10),
                    Margin = new Thickness(5),
                    Child = new StackPanel
                    {
                        Children = {
                        new TextBlock { Text = "Type: " + item.Type},
                        new TextBlock { Text = "Driver Name: " + item.DriverName },
                        new TextBlock { Text = "Cost Estimate: " + item.CostEstimate },
                        new TextBlock { Text = "Driver Phone: " + item.DriverPhone },
                        new TextBlock { Text = "Trip Infor: " + item.TripInfor },
                        //new TextBlock {Text = "Present Position: " + item.PresentPosition}
                    }
                    }
                };
                this.CardContainer.Children.Add(card);
            }
        }


    }
}
