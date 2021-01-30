using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NeutronGame
{
    internal class MoveToken
    {
        private readonly Ellipse ellipseSelected;
        private const int DEFAULT_ANIMATION_SPEED = 500;

        private readonly int goToCol;
        private readonly int goToRow;

        public MoveToken(object sender, Ellipse ellipseSelected, EnumMoveDirection moveDirection)
        {
            this.ellipseSelected = ellipseSelected;

            goToCol = Grid.GetColumn(sender as Button);
            goToRow = Grid.GetRow(sender as Button);

            if (moveDirection == EnumMoveDirection.Diag)
            {
                MoveDiag();
            }
            else if (moveDirection == EnumMoveDirection.Vertical)
            {
                MoveVertically();
            }
            else if (moveDirection == EnumMoveDirection.Horizontal)
            {
                MoveHorizontally();
            }
        }

        #region Move Vertically
        private async void MoveVertically()
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
                    await Task.Delay(DEFAULT_ANIMATION_SPEED);
                }
                else if (y > 0)
                {
                    Grid.SetRow(ellipseSelected, currentRow + 1);
                    await Task.Delay(DEFAULT_ANIMATION_SPEED);
                }
            }
        }
        #endregion

        #region Move Horizontally
        private async void MoveHorizontally()
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
                    await Task.Delay(DEFAULT_ANIMATION_SPEED);
                }
                else if (x > 0)
                {
                    Grid.SetColumn(ellipseSelected, currentCol + 1);
                    await Task.Delay(DEFAULT_ANIMATION_SPEED);
                }
            }
        }
        #endregion

        #region Move Diag
        private async void MoveDiag()
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
                    await Task.Delay(DEFAULT_ANIMATION_SPEED);
                }
                else if (y > 0)
                {
                    Grid.SetRow(ellipseSelected, currentRow + 1);
                    await Task.Delay(DEFAULT_ANIMATION_SPEED);
                }
            }
        }
        #endregion
    }
}