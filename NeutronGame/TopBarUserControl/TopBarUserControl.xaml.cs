using System.Windows;
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
            AddMouseUpEventToEllipse();
            AddDragAndDropEventToRectangle();
            AddButtonCloseHoverEvent();
            AddButtonMinimizeHoverEvent();
        }

        #region Button Minimize Hover Event
        private void AddButtonMinimizeHoverEvent()
        {
            ButtonMinimize.MouseEnter += (s, e) => ButtonMinimize.Width = ButtonMinimize.Height = 13;
            ButtonMinimize.MouseLeave += (s, e) => ButtonMinimize.Width = ButtonMinimize.Height = 10;
        }
        #endregion

        #region Button Close Hover Event
        private void AddButtonCloseHoverEvent()
        {
            ButtonClose.MouseEnter += (s, e) => ButtonClose.Width = ButtonClose.Height = 13;
            ButtonClose.MouseLeave += (s, e) => ButtonClose.Width = ButtonClose.Height = 10;
        }
        #endregion

        #region Add Drag And Drop Event To Rectangle
        private void AddDragAndDropEventToRectangle()
        {
            RectangleDragZone.MouseDown += (s, e) =>
            {
                if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                    (Application.Current.Windows[0] as MainWindow).DragMove();
            };
        }
        #endregion

        #region Add Mouse Up Event To Ellipse
        private void AddMouseUpEventToEllipse()
        {
            ButtonClose.MouseLeftButtonUp += (s, e) => Application.Current.Shutdown();
            ButtonMinimize.MouseLeftButtonUp += (s, e) => (Application.Current.Windows[0] as MainWindow).WindowState = WindowState.Minimized;
        }
        #endregion
    }
}
