using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NeoDisplay
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataListViewPageOriginal : ContentPage
    {
        public RootNeoObject CurentData { get; set; }
        ObservableCollection<Near_Earth_Objects> NeoCollection { get; set; }

        public delegate void UpdateDataListViewPageHandler();
        public event UpdateDataListViewPageHandler UpdateListview;

        public DataListViewPageOriginal()
        {
            BindingContext = this;
            NeoCollection = new ObservableCollection<Near_Earth_Objects>();

            try
            {
                InitializeComponent();
                NeoListView.BindingContext = NeoCollection;
            }
            catch (Exception e)
            { }

            ReloadDataButton_Clicked(this, new EventArgs());
        }

        private async void NeoListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            DetailPage.ObjectToDisplay = NeoCollection[e.ItemIndex];
            await Navigation.PushAsync(new DetailPage());
        }

        private async void ReloadDataButton_Clicked(object sender, EventArgs e)
        {
            string data = "";
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

            /*
            //following use block from https://www.youtube.com/watch?v=_4Usvzh9Gn0 and https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/data/files?tabs=windows
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(DataListViewPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("NeoDisplay.NeoTestData.json");
            using (StreamReader sr = new StreamReader(stream))
            {
                data = sr.ReadToEnd();
            }
            */

            CurentData = JsonConvert.DeserializeObject<RootNeoObject>(data);
            NeoCollection = new ObservableCollection<Near_Earth_Objects>(CurentData.near_earth_objects);
            MainThread.BeginInvokeOnMainThread(() => { NeoCollection.Add(new Near_Earth_Objects()); });
            MainThread.BeginInvokeOnMainThread(() => { NeoCollection.Remove(new Near_Earth_Objects()); });
        }
    }
}