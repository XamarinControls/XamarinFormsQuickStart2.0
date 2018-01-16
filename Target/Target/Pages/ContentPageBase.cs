﻿using Autofac;
using Plugin.Connectivity;
using Plugin.Toasts;
using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;
using Target.Models;
using Target.ViewModels;
using Xamarin.Forms;

namespace Target.Pages
{
    public class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel>, IContentPageBase where TViewModel : class
    {
        protected IBindingTypeConverter bindingDoubleToIntConverter;
        protected IBindingTypeConverter bindingIntToDoubleConverter;
        protected IBindingTypeConverter reverseBoolConverter;
        //protected Settings setting;
        public ContentPageBase() : base()
        {
            bindingDoubleToIntConverter = (IBindingTypeConverter)App.Container.Resolve<IDoubleToIntConverter>();
            bindingIntToDoubleConverter = (IBindingTypeConverter)App.Container.Resolve<IIntToDoubleConverter>();
            reverseBoolConverter = (IBindingTypeConverter)App.Container.Resolve<IReverseBoolConverter>();
            //var _settingsFactory = App.Container.Resolve<ISettingsFactory>();
            //setting = _settingsFactory.GetSettings();
        }
        public double GetSquaredImageSize(int x)
        {
            return (double)(x * 3);
        }
        public async void ShowToast(INotificationOptions options)
        {
            var notificator = DependencyService.Get<IToastNotificator>();

            if (notificator != null)
            {
                var result = await notificator.Notify(options);
            }
        }
        protected override bool OnBackButtonPressed()
        {
            if(typeof(TViewModel) == typeof(GoodByePageViewModel))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
}
