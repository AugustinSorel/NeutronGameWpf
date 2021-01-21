using System.Windows.Controls;

namespace NeutronGame
{
    /// <summary>
    /// Interaction logic for TopBarUserControl.xaml
    /// </summary>
    public partial class TopBarUserControl : UserControl
    {
        #region TopBarUserControl Instance fields

        private static TopBarUserControl _instance;
        /// <summary>
        /// return the Instance of the user control InputUserControl
        /// </summary>
        public static TopBarUserControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TopBarUserControl();

                return _instance;
            }
        }

        #endregion

        public TopBarUserControl()
        {
            InitializeComponent();
        }
    }
}
