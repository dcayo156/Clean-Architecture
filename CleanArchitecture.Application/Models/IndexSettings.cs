namespace CleanArchitecture.Application.Models
{
    public class IndexSettings
    {
        public string ElasticsearchSrv { get; set; } = string.Empty;
        public int? Shards { get; set; }
        public int? Replicas { get; set; }
        public string IndexName { get; set; } = string.Empty;
    }
}

