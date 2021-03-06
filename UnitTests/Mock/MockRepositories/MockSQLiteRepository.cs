﻿using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;
using Target.Models;
using System.Linq;
using Target.Factories;

namespace UnitTests.Mock.MockRepositories
{
    public class MockSQLiteRepository : ISQLiteRepository
    {
        IDefaultsFactory defaultsFactory;
        public MockSQLiteRepository(IDefaultsFactory defaultsFactory)
        {
            this.defaultsFactory = defaultsFactory;
        }
        public async Task<Unit> Create<T>(string name, T obj)
        {
            var settingsFactory = new SettingsFactory(defaultsFactory);
            settingsFactory.SaveSettings(obj as Settings);
            Unit returnval;
            await Task.Delay(TimeSpan.FromMilliseconds(0));
            returnval = Unit.Default;
            return returnval;
        }

        public Task<T> Get<T>(string name)
        {
            // Task<T>.Factory.StartNew(() => T) is how you return a task

            var setting = new Settings()
            {
                AgreedToTermsDate = "",
                FontSize = defaultsFactory.GetFontSize(),
                IsManualFont = defaultsFactory.GetIsManualFont(),
                ShowConnectionErrors = defaultsFactory.GetShowConnectionErrors()
             };
                var newval = (T)Convert.ChangeType(setting, typeof(Settings));

            return Task<T>.Factory.StartNew(() => newval);
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            // await used so this can method can look like a task
            await Task.Delay(TimeSpan.FromMilliseconds(0));
            var setting = new Settings()
            {
                AgreedToTermsDate = "",
                FontSize = 16,
                IsManualFont = false,
                ShowConnectionErrors = false
            };
            var newval = (T)Convert.ChangeType(setting, typeof(Settings));
            List<T> list = new List<T>();
            list.Add(newval);
            
            return list;
        }
    }
}
