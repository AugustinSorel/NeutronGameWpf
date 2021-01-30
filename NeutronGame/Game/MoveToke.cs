using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NeutronGame
{
    internal class MoveToke
    {
        private readonly Ellipse ellipseSelected;

        public MoveToke(object sender, Ellipse ellipseSelected)
        {
            this.ellipseSelected = ellipseSelected;

            int goToCol = Grid.GetColumn(sender as Button);
            int goToRow = Grid.GetRow(sender as Button);

            int currentRow1 = Grid.GetRow(ellipseSelected);
            int currentCol1 = Grid.GetColumn(ellipseSelected);

            if (Math.Abs(goToRow - currentRow1) > 0 && Math.Abs(goToCol - currentCol1) > 0)
            {
                MoveDiag(goToCol, goToRow);
            }
            else if (Math.Abs(goToRow - currentRow1) > 0)
            {
                MoveVertically(goToRow);
            }
            else if (Math.Abs(goToCol - currentCol1) > 0)
            {
                MoveHorizontally(goToCol);
            }
        }

        private async void MoveVertically(int goToRow)
        {
            while (true)
            {
                int currentRow = Grid.GetRow(ellipseSelected);
                int y = goToRow - currentRow;

                if (y == 0)
                    break;

                if (y < 0)
                {
                    Grid.SetRow(ellipseSelected, currentRow - 1);
                    await Task.Delay(500);
                }
                else if (y > 0)
                {
                    Grid.SetRow(ellipseSelected, currentRow + 1);
                    await Task.Delay(500);
                }
            }
        }

        private async void MoveHorizontally(int goToCol)
        {
            while (true)
            {
                int currentCol = Grid.GetColumn(ellipseSelected);
                int x = goToCol - currentCol;

                if (x == 0)
                    break;

                if (x < 0)
                {
                    Grid.SetColumn(ellipseSelected, currentCol - 1);
                    await Task.Delay(500);
                }
                else if (x > 0)
                {
                    Grid.SetColumn(ellipseSelected, currentCol + 1);
                    await Task.Delay(500);
                }
            }
        }

        private async void MoveDiag(int goToCol, int goToRow)
        {
            while (true)
            {
                int currentRow = Grid.GetRow(ellipseSelected);
                int currentCol = Grid.GetColumn(ellipseSelected);

                int y = goToRow - currentRow;
                int x = goToCol - currentCol;

                if (y == 0 && x == 0)
                    break;

                if (x < 0)
                    Grid.SetColumn(ellipseSelected, currentCol - 1);
                else if (x > 0)
                    Grid.SetColumn(ellipseSelected, currentCol + 1);

                if (y < 0)
                {
                    Grid.SetRow(ellipseSelected, currentRow - 1);
                    await Task.Delay(500);
                }
                else if (y > 0)
                {
                    Grid.SetRow(ellipseSelected, currentRow + 1);
                    await Task.Delay(500);
                }
            }
        }
    }
}