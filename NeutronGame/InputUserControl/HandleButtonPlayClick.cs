using System.Windows;
using System.Windows.Controls;

namespace NeutronGame
{
    internal class HandleButtonPlayClick
    {
        public HandleButtonPlayClick(PlayersNameError playersNameError, TextBox textBoxPlayer1Name, TextBox textBoxPlayer2Name)
        {
            if (textBoxPlayer1Name.Text.Trim().Length > 0 && textBoxPlayer2Name.Text.Trim().Length > 0)
            {
                SetPlayersNameErrorToEmpty(playersNameError);
                RemoveInpuUserControl(GetMainWindow());
                AddGameUserControl(GetMainWindow());
            }

            playersNameError.PLayer1NameError = textBoxPlayer1Name.Text.Trim().Length < 1 ? "Invalid Name" : "";

            playersNameError.PLayer2NameError = textBoxPlayer2Name.Text.Trim().Length < 1 ? "Invalid Name" : "";
        }

        private void SetPlayersNameErrorToEmpty(PlayersNameError playersNameError)
        {
            playersNameError.PLayer1NameError = playersNameError.PLayer2NameError = "";
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
            mainWindow.container.Children.Remove(InputUserControl.Instance);
        }
    }
}