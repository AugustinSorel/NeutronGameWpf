using System.Windows.Shapes;

namespace NeutronGame
{
    internal class HandleEllipseClick
    {
        private object sender;
        private bool neutronMoved;
        private Ellipse ellipseSelected;

        public HandleEllipseClick(object sender, bool neutronMoved, Ellipse ellipseSelected)
        {
            this.sender = sender;
            this.neutronMoved = neutronMoved;
            this.ellipseSelected = ellipseSelected;
        }
    }
}