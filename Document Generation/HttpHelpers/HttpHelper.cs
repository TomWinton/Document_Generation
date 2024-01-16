using Microsoft.Extensions.Logging;

namespace DocumentGeneration.HttpHelpers
    {
    public partial class HttpHelper
        {
        private ILogger _log;
        public HttpHelper(ILogger log)
            {
            _log = log;
            }
        }
    }