using System;
using System.Collections.Generic;
using System.Text;
using Target.Interfaces;
using UnitTests.Helpers;

namespace UnitTests.ViewModels
{
    
    public class BaseViewModel
    {
        protected MyAutoMockHelper _myHelper;
        protected IDefaultsFactory defaultsFactory;
        public BaseViewModel()
        {
            _myHelper = new MyAutoMockHelper();
            defaultsFactory = _myHelper.GetDefaultsFactory();
        }
    }
}
