using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Target.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoodByePage : ContentPage, IGoodByePage
    {
        public GoodByePage()
        {
            InitializeComponent();
            lblThanks.Text = "Thanks for considering " + Constants.AppName + "!";
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}