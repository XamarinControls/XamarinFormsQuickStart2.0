using Target.Interfaces;
using Target.Models;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Target.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISQLiteRepository iSQLiteRepository;
        private readonly ISettingsFactory settingsFactory;
        private readonly string _KeyName;
        public SettingsService(ISQLiteRepository iSQLiteRepository, ISettingsFactory settingsFactory)
        {
            this.settingsFactory = settingsFactory;
            _KeyName = this.settingsFactory.KeyName;
            this.iSQLiteRepository = iSQLiteRepository;
        }
        public async Task CheckSettings()
        {
          var allSettings = await iSQLiteRepository.GetAll<Settings>();
            var count = 0;
            foreach(var item in allSettings)
            {
                count++;
                break;
            }
            if(count == 0)
            {
                var settings = settingsFactory.GetSettings();
                await CreateSetting(settings);
            }
        }
        public async Task<Unit> ResetToDefaults()
        {
            settingsFactory.SetDefaults();
            var settings = settingsFactory.GetSettings();
            return await iSQLiteRepository.Create<Settings>(_KeyName, settings);
        }
        public async Task<Unit> CreateSetting(Settings settings)
        {
           return await iSQLiteRepository.Create<Settings>(_KeyName, settings);
        }

        public async Task<ISettings> GetSettings()
        {
            return await iSQLiteRepository.Get<Settings>(_KeyName);
        }

    }
}
