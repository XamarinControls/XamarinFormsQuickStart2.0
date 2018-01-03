using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;
using Target.Models;

namespace UnitTests.MockServices
{
    public class MockSettingsService : ISettingsService
    {
        public Task CheckSettings()
        {
            throw new NotImplementedException();
        }

        public Task<Unit> CreateSetting(Settings settings)
        {
            throw new NotImplementedException();
        }

        public Task<ISettings> GetSettings()
        {
            return Task<ISettings>.Factory.StartNew(() => new Settings() {
                AgreedToTermsDate = "",
                FontSize = 16,
                IsManualFont = false,
                ShowConnectionErrors = false
            });

        }

        public Task<Unit> ResetToDefaults()
        {
            throw new NotImplementedException();
        }
    }
}
