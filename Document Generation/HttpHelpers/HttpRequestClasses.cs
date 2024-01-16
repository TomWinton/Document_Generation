using System.Collections.Generic;

namespace DocumentGeneration.HttpHelpers
    {
    public partial class HttpHelper
        {
        public class RequestDocumentParamaters
            {
            public string DocumentName { get; set; }
            public int? Version { get; set; }
            }
        public class RequestRenderDocument
            {
            public string DocumentId { get; set; }
            public IEnumerable<ParameterRequest> Parameters { get; set; }
            }
        public class ParameterRequest
            {
            public string ParameterName { get; set;}
            public object ParameterValue { get; set; }
            }
        }
    }
