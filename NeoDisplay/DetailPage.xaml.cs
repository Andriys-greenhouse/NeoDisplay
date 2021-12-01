using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NeoDisplay
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public static Near_Earth_Objects ObjectToDisplay { get; set; }
        public DetailPage()
        {
            InitializeComponent();
            BindingContext = ObjectToDisplay;
            OrbitalDataStackLayout.BindingContext = ObjectToDisplay.orbital_data;
            OrbitClassStackLayout.BindingContext = ObjectToDisplay.orbital_data.orbit_class;
        }

        private async void ShowCloseApproachDataButton_Clicked(object sender, EventArgs e)
        {
            CloseApproachDataPage.ApproachDataToDisplay = ObjectToDisplay.close_approach_data;
            await Navigation.PushAsync(new CloseApproachDataPage());
        }
    }
}