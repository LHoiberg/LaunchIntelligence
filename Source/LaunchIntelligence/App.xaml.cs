using LaunchIntelligence.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace LaunchIntelligence
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            
            StyleManager.ApplicationTheme = new Windows8Theme();
            base.OnStartup(e);

     
            var window = new MainWindow
            {
                DataContext = new MainViewModel(),
                WindowState = WindowState.Maximized
            };
            window.Show();
        }
    }
}
