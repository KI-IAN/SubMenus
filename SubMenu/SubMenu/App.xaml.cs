using SubMenu.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SubMenu
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new Hotels(new ViewModels.HotelsGroupViewModel());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
