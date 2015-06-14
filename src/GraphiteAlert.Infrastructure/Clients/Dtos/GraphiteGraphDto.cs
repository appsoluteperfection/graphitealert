namespace GraphiteAlert.Infrastructure.Clients.Dtos
{
    public class GraphiteGraphDto
    {
        public int Leaf { get; set; }
        public string Text { get; set; }
        public int Expandable { get; set; }
        public string Id { get; set; }
        public int AllowChildren { get; set; } 
    }
}