using Target.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target.Interfaces
{
    public interface ISettingsFactory
    {
        string KeyName { get; }
        Settings GetSettings();
        void SaveSettings(Settings settings);
        void SetDefaults();
    }
}
