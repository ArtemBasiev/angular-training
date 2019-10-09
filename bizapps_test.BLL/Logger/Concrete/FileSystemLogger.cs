using System;
using bizapps_test.BLL.Logger.Abstract;
using NLog;

namespace bizapps_test.BLL.Logger.Concrete
{
    public class FileSystemLogger: ICustomLogger
    {
        private readonly Type _errorRoot;

        internal FileSystemLogger(Type type)
        {
            _errorRoot = type;
        }

        public void Log(string errorMessage)
        {
            NLog.Logger log = LogManager.GetLogger(_errorRoot.ToString(), _errorRoot);
            log.Error(errorMessage);
        }
    }
}
