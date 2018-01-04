using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;
using Target.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Target.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoodByePage : ContentPageBase<GoodByePageViewModel>, IGoodByePage
    {
        public GoodByePage()
        {
            InitializeComponent();
            ViewModel = (GoodByePageViewModel)App.Container.Resolve<IGoodByePageViewModel>();
            lblThanks.Text = "Thanks for considering " + ViewModel.defaultsFactory.GetAppName() + "!";
        }
    }
}