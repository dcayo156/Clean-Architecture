using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Models;
using Microsoft.Extensions.Options;
using Nest;
using Serilog;
using ILogger = Serilog.ILogger;

namespace CleanArchitecture.Infrastructure.ElasticSearch
{
    public class ContentIndexManager : IContentIndexManager
    {
        private ElasticClient _ESClient;
        private Application.Models.IndexSettings _indexSettings;
        private readonly ILogger _logger = Log.ForContext(typeof(ContentIndexManager));
        public ContentIndexManager() { }
        public ContentIndexManager(IOptions<Application.Models.IndexSettings> indexSettings)
        {
            _indexSettings = indexSettings.Value;
            _indexSettings.IndexName = "digitalfolder1";
            _indexSettings.Shards = 1;
            _indexSettings.Replicas = 0;    
            _indexSettings.ElasticsearchSrv ="http://localhost:9200";
            this._ESClient = ConnectionToES.EsClient(_indexSettings);
        }
        public bool InsertMetadata(DocumentIndex document)
        {
            try
            {
                _logger.Information($"InsertMedata: Id: {document.Id} EmployeeForename: {document.EmployeeForename}  IndexName: {_indexSettings.IndexName}");
                var _response = _ESClient.Index(document, i => i
                                .Index(_indexSettings.IndexName)
                                .Id(document.Id)
                                .Refresh(Elasticsearch.Net.Refresh.True)
                                );

                if (!_response.IsValid)
                    _logger.Error($"Error accessing Elasticsearch to index: {_response.ToString()}");

                return _response.IsValid;
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while inserting metadata: {ex.Message.ToString()}");
                return false;
            }
        }
        public bool UpdateMetadata(DocumentIndex document)
        {
            try
            {
                _logger.Information($"UpdateMetadata: Id: {document.Id} EmployeeForename: {document.EmployeeForename}  IndexName: {_indexSettings.IndexName}");

                if (document == null)
                {
                    _logger.Error($"Cannot update document information on Index. Document cannot be null.");
                    throw new Exception("Cannot update document information on Index. Document cannot be null.");
                }
                    

                var updateResponse = _ESClient.Update<DocumentIndex>(document.Id, u => u.Doc(document));

                if (!updateResponse.IsValid)
                    _logger.Error($"Error accessing Elasticsearch to index: {updateResponse.ToString()}");

                return updateResponse.IsValid;
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while inserting metadata: {ex.Message.ToString()}");
                return false;
            }
        }
        public List<DocumentIndex> SearchMetadata(string filter)
        {            
            var searchResponse = _ESClient.Search<DocumentIndex>(s => s
                .Size(10000)
                .Query(q => q
                    .QueryString(qs => qs
                        .Query(filter)
                    )
                )
            );
            IEnumerable<DocumentIndex> results = searchResponse.Documents;
            return results.ToList();
        }
    }
}
