using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using static DocumentGeneration.HttpHelpers.HttpHelper;
using DocumentGeneration.Blob;
using DocumentGeneration.Render;

namespace Document_Generation.Functions
{
    public partial class DocumentFunctions
    {
        
        [FunctionName("RequestRenderDocument")]
        public async Task<IActionResult> RequestRenderDocumentFunction(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "request-render-document")] HttpRequest req,
        ILogger log)
        {
            setup(log);
            RequestRenderDocument requestRenderDocument;
            try
            {
                // Read the request body
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                // Deserialize the JSON request body to a custom object
                requestRenderDocument = JsonConvert.DeserializeObject<RequestRenderDocument>(requestBody, deserializerSettings);
            }

            catch (Exception ex)
            {
                log.LogError($"An error occurred parsing the request body: {ex.Message}");
                return new BadRequestObjectResult("An error occurred parsing the request body");
            }
            //Convert this to class with outputs maybe?
            if(!_dbHelper.validateDocumentParameters(requestRenderDocument))
                {
                return new BadRequestObjectResult("Missing Paramaters");
                }

            var blobHelper = new Blob(log);
            var document = _dbContext.Documents.Find(requestRenderDocument.DocumentId);
            var documentBytes = blobHelper.retrieveFileFromBlob(document.BlobLocation);
            var renderHelper = new Render(log, documentBytes, requestRenderDocument.Parameters, document.Renderer);
            /* Local debug
            File.Delete("beep.docx");
            File.WriteAllBytes("beep.docx" , Convert.FromBase64String(Convert.ToBase64String(renderHelper.executeRender())));
            */
            return _httpHelper.documentContent(Convert.ToBase64String(renderHelper.executeRender()));

        }
    }

}