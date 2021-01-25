using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeutronGame
{
    public class Players : INotifyPropertyChanged
    {
        #region PlayersName field
        private string _player1Name;
        private string _player2Name;

        public string Player1Name
        {
            get { return _player1Name; }
            set 
            { 
                _player1Name = value; 
                OnPropertyChanged();
            }
        }

        public string Player2Name
        {
            get { return _player2Name; }
            set 
            {
                _player2Name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
