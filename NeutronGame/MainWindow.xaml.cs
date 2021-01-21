using System.Windows;
using System.Windows.Controls;

namespace NeutronGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region ctor
        public MainWindow()
        {
            InitializeComponent();
            AddInputUserContorl();
            AddTopBarUserControl();
            FocusTextBoxPlayer1Name();
        }
        #endregion

        #region Add TopBarUserControl
        private void AddTopBarUserControl()
        {
            container.Children.Add(TopBarUserControl.Instance);
            Grid.SetColumnSpan(TopBarUserControl.Instance, 3);
        }
        #endregion

        #region Focus TextBox Player1Name
        private void FocusTextBoxPlayer1Name()
        {
            InputUserControl.Instance.TextBoxPlayer1Name.Focus();
            InputUserControl.Instance.TextBoxPlayer1Name.SelectAll();
        }
        #endregion

        #region Add InputUserContorl
        private void AddInputUserContorl()
        {
            container.Children.Add(InputUserControl.Instance);
            Grid.SetColumn(InputUserControl.Instance, 1);
            Grid.SetRow(InputUserControl.Instance, 1);
        }
        #endregion
    }
}