using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using Target.Models;

namespace Target.Interfaces
{
    public interface IOrganizationPageViewModel
    {
        ReactiveList<BaseListItem> Items { get; set; }
    }
}