using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using System.Globalization;

namespace NeoDisplay
{
    public partial class App : Application
    {
        public static RootNeoObject CurentData { get; set; }
        public static ObservableCollection<Near_Earth_Objects> NeoCollection { get; set; }
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
            if (!Properties.ContainsKey("FirstStarted"))
            {
                Properties.Add("FirstStarted", DateTime.Now);
                Properties.Add("LastStarted", DateTime.Now);

                //default values
                Settings = Settings = new ObservableCollection<bool>() { true, false, false, false, true, false };
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(DataListViewPage)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("NeoDisplay.NeoTestData.json");
                using (StreamReader sr = new StreamReader(stream))
                {
                    CurentData = JsonConvert.DeserializeObject<RootNeoObject>(sr.ReadToEnd());
                }

                await SecureStorage.SetAsync("Data", JsonConvert.SerializeObject(CurentData));
                await SecureStorage.SetAsync("Settings", JsonConvert.SerializeObject(Settings));
            }
            else
            {
                try
                {
                    Settings = JsonConvert.DeserializeObject<ObservableCollection<bool>>(await SecureStorage.GetAsync("Settings"));
                }
                catch (Exception e)
                {
                    Settings = new ObservableCollection<bool>() { true, false, false, false, true, false };
                }

                try
                {
                    CurentData = JsonConvert.DeserializeObject<RootNeoObject>(await SecureStorage.GetAsync("Data"));
                }
                catch (Exception e)
                {
                    var assembly = IntrospectionExtensions.GetTypeInfo(typeof(DataListViewPage)).Assembly;
                    Stream stream = assembly.GetManifestResourceStream("NeoDisplay.NeoTestData.json");
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        CurentData = JsonConvert.DeserializeObject<RootNeoObject>(sr.ReadToEnd());
                    }
                }
            }

            NeoCollection = new ObservableCollection<Near_Earth_Objects>(CurentData.near_earth_objects);
        }

        protected async override void OnSleep()
        {
            Properties["LastStarted"] = DateTime.Now;
            await SecureStorage.SetAsync("Data", JsonConvert.SerializeObject(CurentData));
            await SecureStorage.SetAsync("Settings", JsonConvert.SerializeObject(Settings));
        }

        protected override void OnResume()
        {
        }

        public static void OrderNeoCollection()
        {
            if (Settings[5])
            {
                if (Settings[0]) { NeoCollection = new ObservableCollection<Near_Earth_Objects>(NeoCollection.OrderByDescending(x => x.name_limited)); }
                else if (Settings[1]) { NeoCollection = new ObservableCollection<Near_Earth_Objects>(NeoCollection.OrderByDescending(x => double.Parse(x.EstimatedDiametrAverage))); }
                else if (Settings[2]) { { NeoCollection = new ObservableCollection<Near_Earth_Objects>(NeoCollection.OrderByDescending(x => x.is_potentially_hazardous_asteroid)); } }
                else if (Settings[3]) { NeoCollection = new ObservableCollection<Near_Earth_Objects>(NeoCollection.OrderByDescending(x => DateTime.ParseExact(x.orbital_data.first_observation_date,"yyyy-MM-dd", CultureInfo.CurrentCulture))); }
            }
            else if (Settings[4])
            {
                if (Settings[0]) { NeoCollection = new ObservableCollection<Near_Earth_Objects>(NeoCollection.OrderBy(x => x.name_limited)); }
                else if (Settings[1]) { NeoCollection = new ObservableCollection<Near_Earth_Objects>(NeoCollection.OrderBy(x => double.Parse(x.EstimatedDiametrAverage))); }
                else if (Settings[2]) { NeoCollection = new ObservableCollection<Near_Earth_Objects>(NeoCollection.OrderBy(x => x.is_potentially_hazardous_asteroid)); }
                else if (Settings[3]) { NeoCollection = new ObservableCollection<Near_Earth_Objects>(NeoCollection.OrderBy(x => DateTime.ParseExact(x.orbital_data.first_observation_date, "yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")))); }
            }

            NeoCollection.Add(new Near_Earth_Objects());
            NeoCollection.Remove(new Near_Earth_Objects());
        }
    }
}
