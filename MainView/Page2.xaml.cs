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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            //InitializeComponent();
        }

        pprivate void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            
            var radioButton = sender as RadioButton;
            if (radioButton == null) return;

            switch (radioButton.Content.ToString())
            {
                case "Move - Delivery":
                    this.NavigationService.Navigate(new Page2());
                    break;

                case "Home":
                    User_MainWindow mainWindow = new User_MainWindow();
                    mainWindow.Show();
                    Window.GetWindow(this).Close();
                    break;
                default:
                    break;

            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Page22());
        }
    }
}
