using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bizapps_test.BLL.Logger.Abstract
{
    public interface ILogFactory
    {
        ICustomLogger CreateLogger(Type type);
    }
}
