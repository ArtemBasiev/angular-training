using System;
using bizapps_test.BLL.Logger.Abstract;

namespace bizapps_test.BLL.Logger.Concrete
{
    public class FileSystemLogFactory: ILogFactory
    {
        public ICustomLogger CreateLogger(Type type)
        {
            return new FileSystemLogger(type);
        }
    }
}
