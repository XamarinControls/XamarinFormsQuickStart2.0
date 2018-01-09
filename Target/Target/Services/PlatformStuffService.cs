using System;
using System.Collections.Generic;
using System.Text;
using Target.Interfaces;
using Xamarin.Forms;

namespace Target.Services
{
    public class PlatformStuffService : IPlatformStuffService
    {
        private string _baseUrl;
        public PlatformStuffService()
        {
            _baseUrl = DependencyService.Get<IPlatformStuff>().GetBaseUrl();
        }

        public string GetBaseUrl()
        {
            return _baseUrl;
        }
    }
}
