using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Target.Interfaces
{
    public interface ILoginViewModel
    {
        string Greeting { get; set; }
        string ToastMessage { get; set; }
        ReactiveCommand<Unit, Unit> LoginCommand { get; }
    }
}
