using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NeutronGame
{
    /// <summary>
    /// Interaction logic for GameUserControl.xaml
    /// </summary>
    public partial class GameUserControl : UserControl
    {
        #region GameUserControl Instance fields

        private static GameUserControl _instance;
        /// <summary>
        /// return the Instance of the GameUserControl
        /// </summary>
        public static GameUserControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameUserControl();

                return _instance;
            }
        }

        #endregion

        #region ctor
        public GameUserControl()
        {
            InitializeComponent();
            InitGameBoard();
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
                            EllipseStyle = FindResource("EllipsePlayer1") as Style;
                        else if (i == 4)
                            EllipseStyle = FindResource("EllipsePlayer2") as Style;
                        else
                            EllipseStyle = FindResource("EllipseNeutron") as Style;

                        Ellipse ellipse = new Ellipse();
                        ellipse.Style = EllipseStyle;
                        GameBoard.Children.Add(ellipse);
                        Grid.SetColumn(ellipse, j);
                        Grid.SetRow(ellipse, i);
                    }
                }
            }
        }
        #endregion
    }
}
