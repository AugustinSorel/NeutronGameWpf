using System.Windows.Media;

namespace NeutronGame
{
    public static class GlobalColors
    {
        #region Const Colors

        private const string LIGHT_GRAY = "#bdc3c7";
        private const string RED = "#e74c3c";

        private const string TOKEN_PLAYER1 = "#e74c3c";
        private const string TOKEN_PLAYER2 = "#2ecc71";
        private const string TOKEN_NEUTRON = "#f39c12";

        #endregion

        #region Tokens Color

        public static SolidColorBrush TokenPlayer1
        {
            get { return (SolidColorBrush)new BrushConverter().ConvertFrom(TOKEN_PLAYER1); }
        }

        public static SolidColorBrush TokenPlayer2
        {
            get { return (SolidColorBrush)new BrushConverter().ConvertFrom(TOKEN_PLAYER2); }
        }

        public static SolidColorBrush TokenNeutron
        {
            get { return (SolidColorBrush)new BrushConverter().ConvertFrom(TOKEN_NEUTRON); }
        }

        #endregion

        public static SolidColorBrush Red
        {
            get { return (SolidColorBrush)new BrushConverter().ConvertFrom(RED); }
        }

        public static SolidColorBrush LightGray
        {
            get { return (SolidColorBrush)new BrushConverter().ConvertFrom(LIGHT_GRAY); }
        }

    }
}
