using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using DocumentGeneration.DBHelpers;
using System.IO;
using static DocumentGeneration.HttpHelpers.HttpHelper;


namespace Document_Generation.Functions
{
    public partial class DocumentFunctions
    {

        [FunctionName("RequestDocumentParamaters")]
        public async Task<IActionResult> RequestDocumentParamatersFunction(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "retrieve-document-paramaters")] HttpRequest req,
        ILogger log)
        {
            setup(log);
            RequestDocumentParamaters requestDocumentParamaters;
            try
            {
                // Read the request body
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                // Deserialize the JSON request body to a custom object
                requestDocumentParamaters = JsonConvert.DeserializeObject<RequestDocumentParamaters>(requestBody, deserializerSettings);
            }
            catch (Exception ex)
            {
                log.LogError($"An error occurred parsing the request body: {ex.Message}");
                return new BadRequestObjectResult("An error occurred parsing the request body");
            }
            var documentMatch = _dbHelper.retrieveDocument(requestDocumentParamaters.DocumentName, requestDocumentParamaters.Version);
            if (!documentMatch.Any())
            {
                return _httpHelper.notFound($"No document found.");
            }
            Document document = documentMatch.Single();
            var documentParamaters = _dbHelper.getDocumentParameters(document.DocumentID);
            if (!documentParamaters.Any())
            {
                return _httpHelper.notFound($"No parameters found for document: {document.DocumentID}");
            }
            return _httpHelper.jsonContent(new DocumentParamaterRequestResponse(document.DocumentID, documentParamaters.ToList()));

        }
    }

}