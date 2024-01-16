using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DocumentGeneration.HttpHelpers;
using DocumentGeneration.DBHelpers;

namespace Document_Generation.Functions
{
    public partial class DocumentFunctions
        {
        private readonly DocumentDbContext _dbContext;
        private HttpHelper _httpHelper;
        private DBHelper _dbHelper;
        private JsonSerializerSettings deserializerSettings = new JsonSerializerSettings
            {
            MissingMemberHandling = MissingMemberHandling.Error
            };
        public DocumentFunctions(DocumentDbContext dbContext)
            {
            _dbContext = dbContext;
            }
        private void setup(ILogger log)
            {
            _httpHelper = new HttpHelper(log);
            _dbHelper = new DBHelper(log , _dbContext);
            }
        }
}