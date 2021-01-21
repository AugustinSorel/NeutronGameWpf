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
            InitializeComponent();
        }
        #endregion

        private void ButtonPlay_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("Test");
        }

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
    }
}
