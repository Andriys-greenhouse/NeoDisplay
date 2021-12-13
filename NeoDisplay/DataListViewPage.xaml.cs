using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

namespace NeoDisplay
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataListViewPage : ContentPage
    {
        public ObservableCollection<Near_Earth_Objects> NeoCollectionConnector
        {
            get { return App.NeoCollection; }
            set { App.NeoCollection = value; }
        }

        public delegate void UpdateDataListViewPageHandler();
        public event UpdateDataListViewPageHandler UpdateListview;

        public DataListViewPage()
        {
            BindingContext = this;

            InitializeComponent();
            NeoListView.ItemsSource = NeoCollectionConnector;

            ReloadDataButton_Clicked(this, new EventArgs());
        }

        private async void NeoListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            DetailPage.ObjectToDisplay = NeoCollectionConnector[e.ItemIndex];
            await Navigation.PushAsync(new DetailPage());
        }

        private async void ReloadDataButton_Clicked(object sender, EventArgs e)
        {
            string data = "";
            using (HttpClient HttpC = new HttpClient())
            {
                data = await HttpC.GetStringAsync(@"https://api.nasa.gov/neo/rest/v1/neo/browse?api_key=IcRSAo8y9yxcuzf6bRSpQ1kPrpBGwqCpBPGaHlt2");
            }
            /*
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://api.nasa.gov/neo/rest/v1/neo/browse?api_key=IcRSAo8y9yxcuzf6bRSpQ1kPrpBGwqCpBPGaHlt2");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                data = sr.ReadToEnd();
            }
            */
            //following use block from https://www.youtube.com/watch?v=_4Usvzh9Gn0 and https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/data/files?tabs=windows
            /*
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(DataListViewPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("NeoDisplay.NeoTestData.json");
            using (StreamReader sr = new StreamReader(stream))
            {
                data = sr.ReadToEnd();
            }
            */

            App.CurentData = JsonConvert.DeserializeObject<RootNeoObject>(data);
            App.NeoCollection = new ObservableCollection<Near_Earth_Objects>(App.CurentData.near_earth_objects);
            App.OrderNeoCollection();
        }
    }
}