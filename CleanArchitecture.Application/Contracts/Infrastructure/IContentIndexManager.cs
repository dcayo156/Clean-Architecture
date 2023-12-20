using CleanArchitecture.Application.Models;

namespace CleanArchitecture.Application.Contracts.Infrastructure
{
    public interface IContentIndexManager
    {
        bool InsertMetadata(DocumentIndex document);
        bool UpdateMetadata(DocumentIndex document);
        List<DocumentIndex> SearchMetadata(string filter);
    }
}
