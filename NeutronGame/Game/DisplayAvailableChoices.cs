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

        private void DrawDown(int startCol, int startRow)
        {
            for (int i = 1; i < 5; i++)
            {
                if (startRow + i > 4)
                    return;

                if (enumBoard[startCol, startRow + i] != EnumBoard.EmptyCell)
                {
                    var elements = GameUserControl.Instance.GameBoard.Children.Cast<Button>().
                                First(e => Grid.GetColumn(e) == startCol && Grid.GetRow(e) == startRow + i - 1);


                    var eelement = GameUserControl.Instance.GameBoard.Children
                           .OfType<Button>()
                           .Where(e => Grid.GetColumn(e) == startCol && Grid.GetRow(e) == startRow + i - 1)
                           .FirstOrDefault();

                    eelement.Style = GameUserControl.Instance.FindResource("SelectedButton") as Style;

                    return;
                }

                if (startRow + i == 4)
                {
                    var eelement = GameUserControl.Instance.GameBoard.Children
                           .OfType<Button>()
                           .Where(e => Grid.GetColumn(e) == startCol && Grid.GetRow(e) == startRow + i)
                           .FirstOrDefault();

                    eelement.Style = GameUserControl.Instance.FindResource("SelectedButton") as Style;

                    return;
                }
            }
        }

        private void DrawUp(int startCol, int startRow)
        {
            for (int i = 1; i < 5; i++)
            {
                if (startRow - i < 0)
                    return;

                if (enumBoard[startCol, startRow - i] != EnumBoard.EmptyCell)
                {
                    var eelement = GameUserControl.Instance.GameBoard.Children
                           .OfType<Button>()
                           .Where(e => Grid.GetColumn(e) == startCol && Grid.GetRow(e) == startRow - i + 1)
                           .FirstOrDefault();

                    eelement.Style = GameUserControl.Instance.FindResource("SelectedButton") as Style;

                    return;
                }

                if (startRow - i == 0)
                {
                    var eelement = GameUserControl.Instance.GameBoard.Children
                           .OfType<Button>()
                           .Where(e => Grid.GetColumn(e) == startCol && Grid.GetRow(e) == startRow - i)
                           .FirstOrDefault();

                    eelement.Style = GameUserControl.Instance.FindResource("SelectedButton") as Style;

                    return;
                }
            }
        }
    }
}