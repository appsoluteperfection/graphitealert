using System.Collections.Generic;

namespace GraphiteAlert.Dtos
{
    public class ItemCollection<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}