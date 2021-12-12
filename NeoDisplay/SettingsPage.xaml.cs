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
    public partial class SettingsPage : ContentPage
    {
        public static ObservableCollection<bool> Settings;
        public SettingsPage()
        {
            InitializeComponent();
            Settings = App.Settings;
            BindingContext = this;
        }
    }
}