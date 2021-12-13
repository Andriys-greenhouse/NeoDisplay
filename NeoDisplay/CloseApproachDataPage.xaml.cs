using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace NeoDisplay
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CloseApproachDataPage : ContentPage
    {
        public static ObservableCollection<Close_Approach_Data> ApproachDataToDisplay { get; set; }
        public CloseApproachDataPage()
        {
            BindingContext = this;
            InitializeComponent();
            CloseApproachListView.ItemsSource = ApproachDataToDisplay;
        }
    }
}