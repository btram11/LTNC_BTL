using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Page22.xaml
    /// </summary>
    public partial class Page22 : Page
    {
        public Page22()
        {
            InitializeComponent();

            //var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            //Create Data Grid Item Information

            members.Add(new Member { Type = "", CostEstimate = "", DriverPhone = "", TripInfor = "" });

            //Assign Data to Data Grid
            memberDataGrid.ItemsSource = members;
        }



        private void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Button = sender as Button;
            if (Button == null) return;
            switch (Button.Content.ToString())
            {
                case ("Go back"):
                    this.NavigationService.Navigate(new Page2());
                    break;
                case ("Done"):
                    //chuyen ket qua den ??
                    break;
                default:
                    break;
            }

        }
    }

    public class Member
    {
        public Member()
        {
            Type = "None Information";
            TripInfor = "None Information";
            CostEstimate = "0";
            DriverPhone = "None Information";
            //Choose = "None Information";
        }
        public string Type { get; set; }
        public string TripInfor { get; set; }
        public string CostEstimate { get; set; }
        public string DriverPhone { get; set; }
        //public string Choose { get; set; }

    }


}
