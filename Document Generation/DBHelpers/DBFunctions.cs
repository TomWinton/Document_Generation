using System.Linq;
using Microsoft.Extensions.Logging;
using static DocumentGeneration.HttpHelpers.HttpHelper;

namespace DocumentGeneration.DBHelpers
    {
    public class DBHelper
        {
        private ILogger _log;
        private readonly DocumentDbContext _dbContext;

        public DBHelper(ILogger log, DocumentDbContext dbContext)
            {
            _log = log;
            _dbContext = dbContext;
            }
        //This might not want to be a bool, and we might want to validate the types are valid as well
        public bool validateDocumentParameters(RequestRenderDocument requestRenderDocument)
            {
            var documentParameterKeys = getDocumentParameters(requestRenderDocument.DocumentId).Select(a=> a.Paramater).ToList();
            var requestParamaterKeys = requestRenderDocument.Parameters.Select(a=> a.ParameterName).ToList();
            if (documentParameterKeys.Any())
                {
                foreach (var x in documentParameterKeys)
                    {
                        if(!requestParamaterKeys.Contains(x))
                        {
                        return false;
                        }
                    }
                }
            return true;
            }
        public IQueryable<DocumentParamaterRequest> getDocumentParameters (string documentId)
            {
           return _dbContext.Parameters.Where(a => _dbContext.DocumentParameters.Any(b => b.Document == documentId && b.Parameter == a.ParameterID)).Select(
               dp => new DocumentParamaterRequest
                   {
                   Type = dp.TypeNavigation.Name,
                   Paramater = dp.Name
                   }
               );
            }
        public IQueryable<Document> retrieveDocument(string documentName, int? version)
            {
                if(version != null)
                {
                    return _dbContext.Documents.Where(a => a.DocumentName == documentName && a.Version == version);
                }
                else
                {
                    return _dbContext.Documents.Where(a => a.DocumentName == documentName).OrderBy(a => a.Version).Take(1);
                }
            }
        }
    }
