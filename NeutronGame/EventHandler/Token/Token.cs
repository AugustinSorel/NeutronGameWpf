using System;
using System.Windows.Shapes;

namespace NeutronGame
{
    //partial class Test: ResourceDictionary
    partial class EllipseEventHandler
    {
        #region Hover Effect
        private void Ellipse_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Ellipse)sender).Height = 50;
            ((Ellipse)sender).Width = 50;
        }

        private void Ellipse_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Ellipse)sender).Height = 40;
            ((Ellipse)sender).Width = 40;
        }
        #endregion

        #region Click Event

        private void Ellipse_Click(object sender, EventArgs e)
        {
            Game.Instance.HandleTokenClick(sender);
        }

        #endregion
    }
}
