using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            try
            {
                using (WebClient WebC = new WebClient())
                {
                    data = WebC.DownloadString(@"https://api.nasa.gov/neo/rest/v1/neo/browse?api_key=IcRSAo8y9yxcuzf6bRSpQ1kPrpBGwqCpBPGaHlt2");
                }
            }
            catch (Exception g) 
            {

            }
            CurentData = JsonConvert.DeserializeObject<RootNeoObject>(data);
            NeoCollection = new ObservableCollection<Near_Earth_Objects>(CurentData.near_earth_objects);
        }
    }
}