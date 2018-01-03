using Autofac;
using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;

namespace Target.Templates
{
    public class ContentViewBase<TViewModel> : ReactiveContentView<TViewModel> where TViewModel : class
    {
        protected IBindingTypeConverter bindingDoubleToIntConverter;
        protected IBindingTypeConverter bindingIntToDoubleConverter;
        public ContentViewBase() : base()
        {
            bindingDoubleToIntConverter = (IBindingTypeConverter)App.Container.Resolve<IDoubleToIntConverter>();
            bindingIntToDoubleConverter = (IBindingTypeConverter)App.Container.Resolve<IIntToDoubleConverter>();
        }
        protected double GetSquaredImageSize(int x)
        {
            return (double)(x * 3);
        }
    }
}
