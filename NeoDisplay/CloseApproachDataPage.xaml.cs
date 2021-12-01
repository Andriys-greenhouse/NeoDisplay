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
    public partial class CloseApproachDataPage : ContentPage
    {
        public static Close_Approach_Data[] ApproachDataToDisplay { get; set; }
        public CloseApproachDataPage()
        {
            ApproachDataToDisplay = new Close_Approach_Data[] { };
            InitializeComponent();
        }
    }
}