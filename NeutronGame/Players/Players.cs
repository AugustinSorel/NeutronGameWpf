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
                if (value == string.Empty)
                {
                    _player1Name = "Invalid Name";
                    OnPropertyChanged();
                }
                else
                {
                    _player1Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Player2Name
        {
            get { return _player2Name; }
            set 
            {
                if (value == string.Empty)
                {
                    _player2Name = "Invalid Name";
                    OnPropertyChanged();
                }
                else
                {
                    _player2Name = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region ctor

        public Players()
        {
            _player1Name = "Player1";
            _player2Name = "Player2";
        }

        #endregion

        #region On Property Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
