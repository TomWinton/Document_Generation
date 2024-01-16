using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace DocumentGeneration.HttpHelpers
    {
    public partial class HttpHelper
        {

        public OkObjectResult jsonContent(object content)
            {
                var okResult = new OkObjectResult(content);
                okResult.ContentTypes.Add("application/json");
                return okResult;
            }
        public OkObjectResult documentContent(object content)
            {
            var okResult = new OkObjectResult(content);
            return okResult;
            }
        public NotFoundResult notFound(string message)
            {
            _log.LogInformation(message);
            return new NotFoundResult();
            }
        }
    }
