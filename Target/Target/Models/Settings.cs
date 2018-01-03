using Target.Interfaces;
using Newtonsoft.Json;
using System;

namespace Target.Models
{
    [JsonObject]
    public class Settings : ISettings
    {
        public bool IsManualFont { get; set; }
        public int FontSize { get; set; }
        public bool ShowConnectionErrors { get; set; }
        public string AgreedToTermsDate { get; set; }

    }
}
