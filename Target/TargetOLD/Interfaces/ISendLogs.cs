using Target.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Target.Interfaces
{
    public interface ISendLogs
    {
        Task<bool> Send(Errors errors);
    }
}
