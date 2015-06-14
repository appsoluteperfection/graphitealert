using System;

namespace GraphiteAlert.Infrastructure.Configuration
{
    public interface IGraphiteSettings
    {
        Uri GetSearchUri(string searchText);
        int MaximumGraphiteChildrenToSeek { get; }
    }
}