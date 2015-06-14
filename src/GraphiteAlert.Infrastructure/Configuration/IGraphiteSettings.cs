using System;

namespace GraphiteAlert.Infrastructure.Configuration
{
    public interface IGraphiteSettings
    {
        Uri GetSearchUri(string searchText);
        Uri GetImageUri(string graphId);
        int MaximumGraphiteChildrenToSeek { get; }
    }
}