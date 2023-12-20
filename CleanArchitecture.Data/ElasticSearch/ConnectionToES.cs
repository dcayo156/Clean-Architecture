using CleanArchitecture.Application.Models;
using Elasticsearch.Net;
using Nest;

namespace CleanArchitecture.Infrastructure.ElasticSearch
{
    public class ConnectionToES
    {
        public static ElasticClient EsClient(CleanArchitecture.Application.Models.IndexSettings indexSettings)
        {
            string elasticSearchSrv = indexSettings.ElasticsearchSrv;
            string indexName = indexSettings.IndexName;
            int? shards = indexSettings.Shards;
            int? replicas = indexSettings.Replicas;

            var _uris = new[]// clusters
               {
                new Uri(elasticSearchSrv)
            };
            var descriptor = new CreateIndexDescriptor(indexName)
                   .Settings(s => s
                       .NumberOfShards(shards)
                       .NumberOfReplicas(replicas)
                       .Analysis(analysis => analysis
                           .Analyzers(analyzers => analyzers
                               .Custom("folding-analyzer", c => c
                                   .Tokenizer("standard")
                                   .Filters("lowercase", "documentProperties-preserve")
                               )
                           )
                           .TokenFilters(tokenfilters => tokenfilters
                               .AsciiFolding("documentProperties-preserve", ft => ft
                                   .PreserveOriginal()
                            ))
                       ))
                  .Map<DocumentIndex>(m => m
                       .Dynamic()
                 );

            var connectionPool = new SniffingConnectionPool(_uris);
            var settings = new ConnectionSettings(new Uri(elasticSearchSrv))
                 .DefaultIndex(indexName)
                 .EnableApiVersioningHeader();
            //.EnableDebugMode(); 

            var client = new ElasticClient(settings);

            if (!client.Ping().IsValid)
                throw new Exception("Connection to Elasticsearch server failed. Please check url configuration.");

            if (!client.Indices.Exists(indexName).Exists)
            {
                var a = client.Indices.Create(descriptor).DebugInformation;
            }
            //if (!client.Indices.Exists(indexName).Exists)
            //    throw new Exception(client.Indices.Create(descriptor).DebugInformation);

            return client;
        }        
    }
}
