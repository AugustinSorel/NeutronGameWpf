using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i == 0 || i == 4 || (i == 2 && j == 2))
                    {
                        Style EllipseStyle;

                        if (i==0)
                            EllipseStyle = this.FindResource("EllipsePlayer1") as Style;
                        else if (i==4)
                            EllipseStyle = this.FindResource("EllipsePlayer2") as Style;
                        else
                            EllipseStyle = this.FindResource("EllipseNeutron") as Style;

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

        private void Ellipse_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Ellipse)sender).Height = 50;
            ((Ellipse)sender).Width = 50;
        }

        private void Ellipse_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

            ((Ellipse)sender).Height = 40;
            ((Ellipse)sender).Width = 40;
        }

        private void Ellipse_MouseEnter_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Ellipse)sender).Height = 50;
            ((Ellipse)sender).Width = 50;
        }

        private void Ellipse_MouseLeave_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Ellipse)sender).Height = 40;
            ((Ellipse)sender).Width = 40;
        }
    }
}
