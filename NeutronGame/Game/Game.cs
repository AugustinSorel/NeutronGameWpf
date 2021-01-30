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

        #endregion

        #region Start New Game

        public void StartNewGame(GameUserControl gameUserControl)
        {
            this.gameUserControl = gameUserControl;
            InitGameBoard();
            SetGameVariable();
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
            player1Turn = true;
            tokenSelected = false;
            gameTimer = new GameTimer(gameUserControl);
        }
        #endregion

        #region Handle Token click Event

        public async void HandleTokenClick(object sender)
        {
            if (sender is Button)
            {
                if (!tokenSelected)
                    return;

                var goToCol = Grid.GetColumn(sender as Button);
                var goToRow = Grid.GetRow(sender as Button);

                while (true)
                {
                    var currentRow = Grid.GetRow(ellipseSelected);

                    
                    var y = goToRow - currentRow;

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

                while (true)
                {
                    var currentCol = Grid.GetColumn(ellipseSelected);
                    var x = goToCol - currentCol;

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




                //Grid.SetColumn(ellipseSelected, Grid.GetColumn(sender as Button));
                //Grid.SetRow(ellipseSelected, Grid.GetRow(sender as Button));

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
                            EllipseStyle = gameUserControl.FindResource("EllipsePlayer1") as Style;
                        else if (i == 4)
                            EllipseStyle = gameUserControl.FindResource("EllipsePlayer2") as Style;
                        else
                            EllipseStyle = gameUserControl.FindResource("EllipseNeutron") as Style;

                        Ellipse ellipse = new Ellipse
                        {
                            Style = EllipseStyle
                        };
                        gameUserControl.GameBoard.Children.Add(ellipse);
                        Grid.SetColumn(ellipse, j);
                        Grid.SetRow(ellipse, i);
                    }
                }
            }
        }
        #endregion
    }
}
