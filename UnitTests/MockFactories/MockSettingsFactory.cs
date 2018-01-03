using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;
using Target.Models;

namespace UnitTests.MockFactories
{
    class MockSettingsFactory : ISettingsFactory
    {
        public string KeyName => throw new NotImplementedException();

        public Settings GetSettings()
        {
            throw new NotImplementedException();
        }

        public void SaveSettings(Settings settings)
        {
            throw new NotImplementedException();
        }

        public void SetDefaults()
        {
            throw new NotImplementedException();
        }
    }
}
