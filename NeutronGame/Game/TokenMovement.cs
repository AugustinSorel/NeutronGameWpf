using System;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NeutronGame
{
    internal class TokenMovement
    {
        private readonly int goToCol;
        private readonly int goToRow;

        private readonly int currentRow;
        private readonly int currentCol;

        public TokenMovement(object sender, Ellipse ellipseSelected)
        {
            goToCol = Grid.GetColumn(sender as Button);
            goToRow = Grid.GetRow(sender as Button);

            currentRow = Grid.GetRow(ellipseSelected);
            currentCol = Grid.GetColumn(ellipseSelected);
        }

        private bool MoveIsDiagonal()
        {
            return MoveIsHorizontally() && MoveIsVertically();
        }

        private bool MoveIsHorizontally()
        {
            return Math.Abs(goToCol - currentCol) > 0;
        }

        private bool MoveIsVertically()
        {
            return Math.Abs(goToRow - currentRow) > 0;
        }

        internal EnumMoveDirection GetMovementDirection()
        {
            if (MoveIsDiagonal())
            {
                return EnumMoveDirection.Diag;
            }
            else if (MoveIsVertically())
            {
                return EnumMoveDirection.Vertical;
            }
            else
            {
                return EnumMoveDirection.Horizontal;
            }
        }
    }
}