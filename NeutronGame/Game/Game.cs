using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeutronGame
{
    class Game
    {
        #region User Control Fields
        private readonly GameUserControl gameUserControl;
        #endregion

        #region Game Fields

        private bool player1Turn;

        #endregion

        #region ctor
        public Game(GameUserControl gameUserControl)
        {
            this.gameUserControl = gameUserControl;
            InitGameBoard();
            SetGameVariable();
            SetLabelColor();
        }

        private void SetLabelColor()
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
