namespace BCCCompact.Models.Compacts.Bcc.Engines.ComponentMaker
{
    public class IteratorPack
    {
        public BccVertex Vertex { get; set; }
        public Component Component { get; set; }

        public IteratorPack(BccVertex vertex, Component component)
        {
            this.Vertex = vertex;
            this.Component = component;
        }
    }
}
