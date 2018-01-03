using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.Interfaces
{
    public interface ISvgImageSourceConverterForReactive
    {
        int GetAffinityForObjects(Type fromType, Type toType);
        bool TryConvert(object from, Type toType, object conversionHint, out object result);
    }
}
