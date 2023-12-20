namespace CleanArchitecture.Application.Models
{
    public class AppSettings
    {
        public string ElasticsearchSrv { get; set; }
        public int? Shards { get; set; }
        public int? Replicas { get; set; }
        public string IndexName { get; set; }
    }
}
