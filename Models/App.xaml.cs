using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Models
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Manager cute = new Manager();
            cute.MakeNewTrip("245/17 Xô Viết Nghệ Tĩnh, Phường 17, Quận Bình Thạnh, Thành phố Hồ Chí Minh", "10 Lê Đình Thám, Quận Tân Phú, Thành phố Hồ Chí Minh", new DateTime(2000, 1, 3));
            //base.OnStartup(e);
        }
    }
}
