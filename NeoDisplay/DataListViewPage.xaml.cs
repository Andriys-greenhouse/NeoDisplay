using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Reflection;

namespace NeoDisplay
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataListViewPage : ContentPage
    {
        public bool ListViewVisible { get; set; }
        public static RootNeoObject CurentData { get; set; }
        public static ObservableCollection<Near_Earth_Objects> NeoCollection { get; set; }

        public delegate void UpdateDataListViewPageHandler();
        public event UpdateDataListViewPageHandler UpdateDataListViewPage;

        public DataListViewPage()
        {
            InitializeComponent();
            NeoCollection = new ObservableCollection<Near_Earth_Objects>();
            NeoListView.BindingContext = NeoCollection;
            ReloadDataButton_Clicked(this, new EventArgs());

            try
            {
                CurentData = JsonConvert.DeserializeObject<RootNeoObject>((string)Resources["Data"]);
            }
            catch(System.Collections.Generic.KeyNotFoundException e)
            {
                CurentData = new RootNeoObject { near_earth_objects = new Near_Earth_Objects[] { } };
            }
        }

        private async void NeoListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            DetailPage.ObjectToDisplay = NeoCollection[e.ItemIndex];
            await Navigation.PushAsync(new DetailPage());
        }

        private async void ReloadDataButton_Clicked(object sender, EventArgs e)
        {
            string data = "";
            /*
                using (WebClient WebC = new WebClient())
                {
                    data = WebC.DownloadString(@"https://api.nasa.gov/neo/rest/v1/neo/browse?api_key=IcRSAo8y9yxcuzf6bRSpQ1kPrpBGwqCpBPGaHlt2");
                }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://api.nasa.gov/neo/rest/v1/neo/browse?api_key=IcRSAo8y9yxcuzf6bRSpQ1kPrpBGwqCpBPGaHlt2");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                data = sr.ReadToEnd();
            }
                */

            //following use block from https://www.youtube.com/watch?v=_4Usvzh9Gn0 and https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/data/files?tabs=windows
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(DataListViewPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("NeoDisplay.NeoTestData.json");
            using (StreamReader sr = new StreamReader(stream))
            {
                data = sr.ReadToEnd();
            }

            CurentData = JsonConvert.DeserializeObject<RootNeoObject>(data);
            NeoCollection = new ObservableCollection<Near_Earth_Objects>(CurentData.near_earth_objects);
        }
    }
}