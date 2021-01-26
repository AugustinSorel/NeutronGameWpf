using System.Windows;
using System.Windows.Controls;

namespace NeutronGame
{
    internal class HandleButtonPlayClick
    {
        #region ctor
        public HandleButtonPlayClick(string PLayer1Name, string Player2Name)
        {
            if (PLayer1Name.Length > 0 && Player2Name.Length > 0)
            {
                RemoveInpuUserControl(GetMainWindow());
                AddGameUserControl(GetMainWindow());
            }
        }

        #endregion

        #region Get Main Window
        private static MainWindow GetMainWindow()
        {
            return Application.Current.Windows[0] as MainWindow;
        }
        #endregion

        #region Add Game User Control To Main Window Control 
        private static void AddGameUserControl(MainWindow mainWindow)
        {
            mainWindow.container.Children.Add(GameUserControl.Instance);
            Grid.SetColumn(GameUserControl.Instance, 1);
            Grid.SetRow(GameUserControl.Instance, 1);
        }
        #endregion

        #region Remove Inpuse User Control From Main Window
        private static void RemoveInpuUserControl(MainWindow mainWindow)
        {
            mainWindow.container.Children.Remove(InputUserControl.Instance);
        }
        #endregion
    }
}