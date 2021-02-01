using System;
using System.Linq;
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
        private bool neutronMoved;
        private GameTimer gameTimer;
        private Ellipse ellipseSelected;
        EnumBoard[,] enumBoard;
        EnumMoveDirection enumMoveDirection;

        #endregion

        #region Start New Game

        public void StartNewGame(GameUserControl gameUserControl)
        {
            this.gameUserControl = gameUserControl;
            SetGameVariable();
            ResetGameBoard();
            InitGameBoard();
            SetLabelsColor();
        }

        private void ResetGameBoard()
        {
            gameUserControl.GameBoard.Children.RemoveRange(25, 11);
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
            neutronMoved = true;
            gameTimer = new GameTimer(gameUserControl);
        }
        #endregion

        #region Handle Token click Event

        public void HandleTokenClick(object sender)
        {
            if (sender is Button)
            {
                if (IllegalMove(sender))
                    return;

                UpdateEnumBoard(sender);

                if (neutronMoved)
                {
                    player1Turn ^= true;
                    neutronMoved = false;
                }
                else if(!neutronMoved)
                {
                    neutronMoved = true;
                }

                enumMoveDirection = GetMoveDirection(sender);

                new MoveToken(sender, ellipseSelected, enumMoveDirection);

                SetLabelsColor();

                SetAllButtonsStyleToDefault();
                CheckEndGame();
            }
            else if (sender is Ellipse)
            {
                if (Player1SelectPlayer2Token(sender) ||Player2SelectPlayer1Token(sender))
                    return;

                if (neutronMoved && (sender as Ellipse).Style != gameUserControl.FindResource("EllipseNeutron") as Style)
                {
                    SetAllButtonsStyleToDefault();
                    gameTimer.StartTimer();
                    ellipseSelected = sender as Ellipse;
                    DisplayPieceSelected();
                }
                else if (!neutronMoved && (sender as Ellipse).Style == gameUserControl.FindResource("EllipseNeutron") as Style)
                {
                    SetAllButtonsStyleToDefault();
                    gameTimer.StartTimer();
                    ellipseSelected = sender as Ellipse;
                    DisplayPieceSelected();
                }
            }
        }

        private void CheckEndGame()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i == 0)
                    {
                        if (enumBoard[j, i] == EnumBoard.NeutronToken)
                        {
                            MessageBox.Show("Player1 Won !!!!");
                            CleanBoard();
                            return;
                        }
                    }
                    else if (i == 4)
                    {
                        if (enumBoard[j, i] == EnumBoard.NeutronToken)
                        {
                            MessageBox.Show("Player2 Won !!!!");
                            CleanBoard();
                            return;
                        }
                    }
                }
            }
        }

        private void CleanBoard()
        {
            StartNewGame(gameUserControl);
        }

        private void SetAllButtonsStyleToDefault()
        {
            foreach (var button in gameUserControl.GameBoard.Children.OfType<Button>())
                button.Style = gameUserControl.TryFindResource(typeof(Button)) as Style;
        }

        private bool Player2SelectPlayer1Token(object sender)
        {
            return !player1Turn && (sender as Ellipse).Fill.ToString() == GlobalColors.TokenPlayer1.ToString();
        }

        private bool Player1SelectPlayer2Token(object sender)
        {
            return player1Turn && (sender as Ellipse).Fill.ToString() == GlobalColors.TokenPlayer2.ToString();
        }

        private void DisplayPieceSelected()
        {
            new DisplayAvailableChoices(ellipseSelected, enumBoard);
        }

        private EnumMoveDirection GetMoveDirection(object sender)
        {
            TokenMovement tokenMovement = new TokenMovement(sender, ellipseSelected);
            return tokenMovement.GetMovementDirection();
        }

        private void UpdateEnumBoard(object sender)
        {
            enumBoard[Grid.GetColumn(ellipseSelected), Grid.GetRow(ellipseSelected)] = EnumBoard.EmptyCell;

            if (neutronMoved)
            {
                if (player1Turn)
                    enumBoard[Grid.GetColumn(sender as Button), Grid.GetRow(sender as Button)] = EnumBoard.Player1Token;
                else
                    enumBoard[Grid.GetColumn(sender as Button), Grid.GetRow(sender as Button)] = EnumBoard.Player2Token;
            }
            else
            {
                enumBoard[Grid.GetColumn(sender as Button), Grid.GetRow(sender as Button)] = EnumBoard.NeutronToken;
            }
        }

        private bool IllegalMove(object sender)
        {
            int row = Grid.GetRow((sender as Button));
            int col = Grid.GetColumn((sender as Button));

            if (enumBoard[col, row] != EnumBoard.EmptyCell)
            {
                DisplayWrongCellClicked(sender);
                return true;
            }

            if ((sender as Button).Style != gameUserControl.FindResource("SelectedButton") as Style)
            {
                DisplayWrongCellClicked(sender);
                return true;
            }

            return false;
        }

        private void DisplayWrongCellClicked(object sender)
        {
            (sender as Button).Style = gameUserControl.FindResource("WrongButton") as Style;
        }

        #endregion

        #region Init The Default Game Board;

        private void InitGameBoard()
        {
            new InitGameBoard(gameUserControl, enumBoard);
        }
        
        #endregion
    }
}
