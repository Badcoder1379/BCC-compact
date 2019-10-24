namespace BCCCompact.Models
{
    public class BCCEdge
    {
        public int A { get; set; }
        public int B { get; set; }
        public BCCEdge(int a, int b)
        {
            this.A = a;
            this.B = b;
        }
    }
}