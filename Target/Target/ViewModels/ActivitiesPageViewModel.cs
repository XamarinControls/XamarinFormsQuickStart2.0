﻿using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace Target.ViewModels
{
    public class ActivitiesPageViewModel : BaseViewModel, IActivitiesPageViewModel
    {
        
        public ActivitiesPageViewModel(
            ISettingsService settingsService,
            ISettingsFactory settingsFactory,
            IDefaultsFactory defaultsFactory,
            IPlatformStuffService platformStuffService
            )
            : base(settingsService, settingsFactory, defaultsFactory)
        {
            Title = "Activities";
            Greeting = "Activities Page";
        }
    }
}
