using System.Windows;
using System.Windows.Controls;

namespace NeutronGame
{
    internal class HandleButtonPlayClick
    {
        #region ctor
        public HandleButtonPlayClick(TextBox textBoxPlayer1Name, TextBox textBoxPlayer2Name)
        {
            if (textBoxPlayer1Name.Text.Trim().Length > 0 && textBoxPlayer2Name.Text.Trim().Length > 0)
            {
                SetPlayersName(textBoxPlayer1Name, textBoxPlayer2Name);
                RemoveInpuUserControl(GetMainWindow());
                AddGameUserControl(GetMainWindow());
            }
        }

        #endregion
        
        #region Set players Name 
        private void SetPlayersName(TextBox textBoxPlayer1Name, TextBox textBoxPlayer2Name)
        {
            new Players
            {
                Player1Name = textBoxPlayer1Name.Text,
                Player2Name = textBoxPlayer2Name.Text
            };
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