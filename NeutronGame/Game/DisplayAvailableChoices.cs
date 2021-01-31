using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NeutronGame
{
    internal class DisplayAvailableChoices
    {
        private readonly EnumBoard[,] enumBoard;

        public DisplayAvailableChoices(Ellipse ellipseSelected, EnumBoard[,] enumBoard)
        {
            this.enumBoard = enumBoard;

            int startCol = Grid.GetColumn(ellipseSelected);
            int startRow = Grid.GetRow(ellipseSelected);


            DrawUp(startCol, startRow);
            DrawDown(startCol, startRow);
        }

        private Button GetAllowedButton(int x, int y)
        {
            Button element = GameUserControl.Instance.GameBoard.Children
                           .OfType<Button>()
                           .Where(e => Grid.GetColumn(e) == x && Grid.GetRow(e) == y)
                           .FirstOrDefault();

            return element;
        }

        #region If Statment

        private bool CellNotEmpty(int x, int y)
        {
            return enumBoard[x, y] != EnumBoard.EmptyCell;
        }

        private bool DistanceBetweenTokenIs1(int x, int y)
        {
            return enumBoard[x, y] != EnumBoard.EmptyCell;
        }

        #endregion

        #region Draw Down
        private void DrawDown(int startCol, int startRow)
        {
            for (int i = 1; i < 5; i++)
            {
                if (startRow + i > 4)
                    return;

                if (DistanceBetweenTokenIs1(startCol, startRow + 1))
                    return;

                if (CellNotEmpty(startCol, startRow + i))
                {
                    GetAllowedButton(startCol, startRow + i - 1).Style = GameUserControl.Instance.FindResource("SelectedButton") as Style;
                    return;
                }

                if (BottomWallHited(startRow, i))
                {
                    GetAllowedButton(startCol, startRow + i).Style = GameUserControl.Instance.FindResource("SelectedButton") as Style;
                    return;
                }
            }
        }

        private bool BottomWallHited(int startRow, int i)
        {
            return startRow + i == 4;
        }
        #endregion

        #region Draw Up
        private void DrawUp(int startCol, int startRow)
        {
            for (int i = 1; i < 5; i++)
            {
                if (startRow - i < 0)
                    return;

                if (DistanceBetweenTokenIs1(startCol, startRow - 1))
                    return;

                if (CellNotEmpty(startCol, startRow - i))
                {
                    GetAllowedButton(startCol, startRow - i + 1).Style = GameUserControl.Instance.FindResource("SelectedButton") as Style;
                    return;
                }

                if (TopWallHited(startRow, i))
                {
                    GetAllowedButton(startCol, startRow - i).Style = GameUserControl.Instance.FindResource("SelectedButton") as Style;
                    return;
                }
            }
        }

        private static bool TopWallHited(int startRow, int i)
        {
            return startRow - i == 0;
        }

        #endregion 
    }
}