using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeutronGame
{
    class Game
    {
        #region User Control Fields
        private GameUserControl gameUserControl;
        #endregion

        #region Game Instance

        private static Game _instance;

        public static Game Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Game();

                return _instance;
            }
        }

        #endregion

        #region Game Fields

        private bool player1Turn;
        private bool tokenSelected;
        private GameTimer gameTimer;
        private Ellipse ellipseSelected;
        EnumBoard[,] enumBoard;

        #endregion

        #region Start New Game

        public void StartNewGame(GameUserControl gameUserControl)
        {
            this.gameUserControl = gameUserControl;
            SetGameVariable();
            InitGameBoard();
            SetLabelsColor();
        }

        private void SetLabelsColor()
        {
            if (player1Turn)
            {
                gameUserControl.LabelPlayer1Name.Foreground = Brushes.White;
                gameUserControl.LabelPlayer2Name.Foreground = GlobalColors.LightGray;
            }
            else
            {
                gameUserControl.LabelPlayer1Name.Foreground = GlobalColors.LightGray;
                gameUserControl.LabelPlayer2Name.Foreground = Brushes.White;
            }
                
        }

        private void SetGameVariable()
        {
            enumBoard = new EnumBoard[5, 5];
            player1Turn = true;
            tokenSelected = false;
            gameTimer = new GameTimer(gameUserControl);
        }
        #endregion

        #region Handle Token click Event

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

        public void HandleTokenClick(object sender)
        {
            if (sender is Button)
            {
                if (!tokenSelected)
                    return;

                if (IllegalMove())
                    return;

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

                player1Turn ^= true;
                SetLabelsColor();
                tokenSelected = false;

            }
            else if (sender is Ellipse)
            {
                if (!tokenSelected)
                {
                    if (player1Turn && (sender as Ellipse).Fill.ToString() == GlobalColors.TokenPlayer2.ToString() ||
                        !player1Turn && (sender as Ellipse).Fill.ToString() == GlobalColors.TokenPlayer1.ToString())
                    {
                        MessageBox.Show("Wrong Piece");
                        return;
                    }

                    gameTimer.StartTimer();
                    tokenSelected = true;
                    ellipseSelected = sender as Ellipse;
                }
                else
                {
                    tokenSelected = false;
                }
            }
        }

        private bool IllegalMove()
        {
            foreach (var item in enumBoard)
            {
                MessageBox.Show(item.ToString());
            }

            return false;
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

        #endregion

        #region Init The Default Game Board;
        private void InitGameBoard()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i == 0 || i == 4 || (i == 2 && j == 2))
                    {

                        Style EllipseStyle;

                        if (i == 0)
                        {
                            EllipseStyle = gameUserControl.FindResource("EllipsePlayer1") as Style;
                            enumBoard[i, j] = EnumBoard.Player1Token;
                        }
                        else if (i == 4)
                        {
                            EllipseStyle = gameUserControl.FindResource("EllipsePlayer2") as Style;
                            enumBoard[i, j] = EnumBoard.Player2Token;
                        }
                        else
                        {
                            EllipseStyle = gameUserControl.FindResource("EllipseNeutron") as Style;
                            enumBoard[i, j] = EnumBoard.NeutronToken;
                        }

                        Ellipse ellipse = new Ellipse
                        {
                            Style = EllipseStyle
                        };
                        gameUserControl.GameBoard.Children.Add(ellipse);
                        Grid.SetColumn(ellipse, j);
                        Grid.SetRow(ellipse, i);
                    }
                    else
                        enumBoard[i, j] = EnumBoard.EmptyCell;
                }
            }
        }
        #endregion
    }

    public enum EnumBoard
    {
        Player1Token,
        Player2Token,
        NeutronToken,
        EmptyCell,
    }
}
