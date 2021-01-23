using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeutronGame
{
    /// <summary>
    /// hold the error if the user enter a wrong name
    /// </summary>
    public class PlayersNameError : INotifyPropertyChanged
    {
        #region Players Name Error Fields
        private string _player1NameError;
        private string _player2NameError;

        public string PLayer1NameError
        {
            get { return _player1NameError; }
            set 
            { 
                _player1NameError = value; 
                NotifyPropertyChanged(); 
            }
        }

        public string PLayer2NameError
        {
            get { return _player2NameError; }
            set 
            { 
                _player2NameError = value; 
                NotifyPropertyChanged(); 
            }
        }

        #endregion

        public PlayersNameError()
        {
            _player1NameError = _player2NameError = "";
        }

        #region Property changed
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}