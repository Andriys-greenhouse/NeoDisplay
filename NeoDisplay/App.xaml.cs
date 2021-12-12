using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace NeoDisplay
{
    public partial class App : Application
    {
        public static RootNeoObject CurentData { get; set; }
        public static ObservableCollection<bool> Settings { get; set; }
        public static string VersionOfApp { get; set; }
        public App()
        {
            VersionOfApp = "0.1";

            OnStart();

            InitializeComponent();

            MainPage = new MainPage();
        }

        protected async override void OnStart()
        {
            if (!Resources.ContainsKey("FirstStarted"))
            {
                Resources.Add("FirstStarted", DateTime.Now);
                Resources.Add("LastStarted", DateTime.Now);

                await SecureStorage.SetAsync("Data", "{ null }");
                await SecureStorage.SetAsync("Settings", "{ null }");
            }
            else
            {
                Resources["LastStarted"] = DateTime.Now;

                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(DataListViewPage)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("NeoDisplay.NeoTestData.json");
                using (StreamReader sr = new StreamReader(stream))
                {
                    await SecureStorage.SetAsync("Data", sr.ReadToEnd());
                }

                try 
                {
                    Settings = JsonConvert.DeserializeObject<ObservableCollection<bool>>(await SecureStorage.GetAsync("Settings"));
                }
                catch (Exception e)
                {
                    Settings = new ObservableCollection<bool>() { true, false, false, false, true, false };
                }

                Settings = new ObservableCollection<bool>() { true, false, false, false, true, false };

            }
        }

        protected async override void OnSleep()
        {
            await SecureStorage.SetAsync("Data", JsonConvert.SerializeObject(CurentData));
            await SecureStorage.SetAsync("Settings", JsonConvert.SerializeObject(SettingsPage.Settings));
        }

        protected override void OnResume()
        {
        }
    }
}
