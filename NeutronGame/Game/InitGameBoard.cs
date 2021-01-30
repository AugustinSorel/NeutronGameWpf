using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NeutronGame
{
    internal class InitGameBoard
    {
        private GameUserControl gameUserControl;
        private EnumBoard[,] enumBoard;

        public InitGameBoard(GameUserControl gameUserControl, EnumBoard[,] enumBoard)
        {
            this.gameUserControl = gameUserControl;
            this.enumBoard = enumBoard;

            GetGameBoard();
        }

        private void GetGameBoard()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (FirstRow(i) || LastRow(i) || MiddleCell(i, j))
                        CreateAToken(i, j);
                    else 
                        SetEnumBoardToEmptyCell(i, j);
                }
            }
        }

        private void SetEnumBoardToEmptyCell(int i, int j)
        {
            enumBoard[j, i] = EnumBoard.EmptyCell;
        }

        private void CreateAToken(int i, int j)
        {
            SetEnumBoard(i, j);

            Ellipse ellipse = new Ellipse
            {
                Style = GetEllipseStyle(i)
            };

            gameUserControl.GameBoard.Children.Add(ellipse);
            Grid.SetColumn(ellipse, j);
            Grid.SetRow(ellipse, i);
        }

        private void SetEnumBoard(int i, int j)
        {
            if (FirstRow(i))
                enumBoard[j, i] = EnumBoard.Player1Token;
            else if (LastRow(i))
                enumBoard[j, i] = EnumBoard.Player2Token;
            else
                enumBoard[j, i] = EnumBoard.NeutronToken;
        }

        private Style GetEllipseStyle(int i)
        {
            if (FirstRow(i))
                return gameUserControl.FindResource("EllipsePlayer1") as Style;
            else if (LastRow(i))
                return gameUserControl.FindResource("EllipsePlayer2") as Style;
            else
                return gameUserControl.FindResource("EllipseNeutron") as Style;
        }

        private bool MiddleCell(int i, int j)
        {
            return (i == 2 && j == 2);
        }

        private bool LastRow(int i)
        {
            return i == 4;
        }

        private bool FirstRow(int i)
        {
            return i == 0;
        }
    }
}