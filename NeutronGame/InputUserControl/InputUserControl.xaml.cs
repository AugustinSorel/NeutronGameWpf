using System.Windows;
using System.Windows.Controls;

namespace NeutronGame
{
    /// <summary>
    /// Interaction logic for InputUserControl.xaml
    /// </summary>
    public partial class InputUserControl : UserControl
    {
        #region InputUserControl Instance fields

        private static InputUserControl _instance;
        /// <summary>
        /// return the Instance of the user control InputUserControl
        /// </summary>
        public static InputUserControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new InputUserControl();

                return _instance;
            }
        }

        #endregion
        
        #region ctor
        public InputUserControl()
        {
            DataContext = Players.Instance;
            InitializeComponent();
        }
        #endregion

        #region ButtonPlay Click
        private void ButtonPlay_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            new HandleButtonPlayClick(TextBoxPlayer1Name.Text.Trim(), TextBoxPlayer2Name.Text.Trim());
        }
        #endregion

        #region ButtonPlay Over Effect
        private void ButtonPlay_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ButtonPlay.TextDecorations = TextDecorations.Underline;
        }

        private void ButtonPlay_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ButtonPlay.TextDecorations = null;
        }
        #endregion

        #region Textbox Player1 Name Enter Press
        private void TextBoxPlayer1Name_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                TextBoxPlayer2Name.Focus();
                TextBoxPlayer2Name.SelectAll();
            }

        }
        #endregion

        #region Textbox Player2 Name Enter Press
        private void TextBoxPlayer2Name_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                ButtonPlay_MouseLeftButtonUp(null, null);
        }
        #endregion
    }
}