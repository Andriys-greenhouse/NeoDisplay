using Xamarin.Forms;
using System;

namespace NeoDisplay
{
    public partial class App : Application
    {
        static bool Started { get; set; }
        public static string VersionOfApp { get; set; }
        public App()
        {
            VersionOfApp = "0.1";

            OnStart();

            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            if (Resources.ContainsKey("Started"))
            {
                Started = (bool)Resources["Started"];
            }
            else { Started = false; }

            if(!Started)
            {
                Resources.Add("FirstStarted", DateTime.Now);
                Resources.Add("LastStarted", DateTime.Now);

                Resources.Add("Data", "");
            }
            else { Resources["LastStarted"] = DateTime.Now; }

            if (!Resources.ContainsKey("Started"))
            {
                Resources.Add("Started", true);
            }
            else { Resources["Started"] = true; }
        }

        protected override void OnSleep()
        {
            Resources["Data"] = DataListViewPage.CurentData;
        }

        protected override void OnResume()
        {
        }
    }
}
