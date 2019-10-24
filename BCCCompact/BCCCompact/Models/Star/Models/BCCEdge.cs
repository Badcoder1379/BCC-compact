namespace BCCCompact.Models
{
    public class BCCEdge
    {
        public int Source { get; set; }
        public int Target { get; set; }
        public BCCEdge(int source, int target)
        {
            this.Source = source;
            this.Target = target;
        }
    }
}
