using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void SwitchTheme(bool isDarkMode)
        {
            var dict = new ResourceDictionary
            {
                Source = new Uri(isDarkMode ? "DarkThemeResources.xaml" : "LightThemeResources.xaml", UriKind.Relative)
            };

            Current.Resources.MergedDictionaries.Clear();
            Current.Resources.MergedDictionaries.Add(dict);
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SwitchTheme(Comp4220.Properties.Settings.Default.IsDarkMode);
        }
    }

}
