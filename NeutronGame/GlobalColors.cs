using System.Windows.Media;

namespace NeutronGame
{
    public static class GlobalColors
    {
        #region Const Colors

        private const string LIGHT_GRAY = "#bdc3c7";
        private const string RED = "#e74c3c";

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
