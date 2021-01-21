﻿using System.Windows;
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

        #region ButtonPlay Click
        private void ButtonPlay_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RemoveInpuUserControl(GetMainWindow());
            AddGameUserControl(GetMainWindow());
        }

        private static MainWindow GetMainWindow()
        {
            return Application.Current.Windows[0] as MainWindow;
        }

        private static void AddGameUserControl(MainWindow mainWindow)
        {
            mainWindow.container.Children.Add(GameUserControl.Instance);
            Grid.SetColumn(GameUserControl.Instance, 1);
            Grid.SetRow(GameUserControl.Instance, 1);
        }

        private static void RemoveInpuUserControl(MainWindow mainWindow)
        {
            mainWindow.container.Children.Remove(_instance);
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
    }
}
