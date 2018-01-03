using System;
using System.Collections.Generic;
using System.Text;

namespace Target.Models
{
    public class ErrorItem
    {
        public string date { get; set; }
        public string issue { get; set; }
        public string thetype { get; set; }

        public override string ToString()
        {
            return $"{date}: {issue}";
        }
    }

    public class Errors
    {
        public List<ErrorItem> ErrorItems { get; set; }
        public string HostName { get; set; }
    }
}
