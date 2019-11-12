namespace BCCCompact.Models
{
    public class BccEdge
    {
        public int Source { get; set; }
        public int Target { get; set; }
        public BccEdge(int source, int target)
        {
            this.Source = source;
            this.Target = target;
        }
    }
}
