namespace BCCCompact.Models.Compacts.BCC.Engines.ComponentMaker
{
    public class IteratorPack
    {
        public BCCVertex vertex;
        public Component component;

        public IteratorPack(BCCVertex vertex, Component component)
        {
            this.vertex = vertex;
            this.component = component;
        }
    }
}
