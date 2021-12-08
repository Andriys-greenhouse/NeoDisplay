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
        public ObservableCollection<bool> Settings {get; set;}
        public SettingsPage()
        {
            Settings = new ObservableCollection<bool>() { false, true, false, false, true, false };
            InitializeComponent();
            BindingContext = this;
        }
    }
}