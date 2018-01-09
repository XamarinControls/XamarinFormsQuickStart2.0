using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Helpers;

namespace UnitTests.ViewModels
{
    
    public class BaseViewModel
    {
        protected MyAutoMockHelper _myHelper;
        public BaseViewModel()
        {
            _myHelper = new MyAutoMockHelper();
        }
    }
}
