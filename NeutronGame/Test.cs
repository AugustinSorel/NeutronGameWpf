using System.Windows;
using System.Windows.Shapes;

namespace NeutronGame
{
    partial class Test: ResourceDictionary
    {
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
    }
}
