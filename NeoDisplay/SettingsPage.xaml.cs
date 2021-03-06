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
        public ObservableCollection<bool> SettingsConnector
        {
            get { return App.Settings; }
            set { App.Settings = value; }
        }
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}