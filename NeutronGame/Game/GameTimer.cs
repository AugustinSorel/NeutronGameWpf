using System.Diagnostics;
using System.Timers;

namespace NeutronGame
{
    internal class GameTimer
    {
        private static Timer _timer;
        private static Stopwatch _stopWatch;

        public GameTimer(GameUserControl gameUserControl)
        {
            _timer = new Timer
            {
                Interval = 1000
            };
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _stopWatch = new Stopwatch();


            _timer.Enabled = false;

            gameUserControl.LabelTimer.Content = "00:00:00";
        }

        internal void StartTimer()
        {
            if (!_timer.Enabled)
            {
                _timer.Start();
                _stopWatch.Start();
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            GameUserControl.Instance.Dispatcher.Invoke(() =>
            {
                GameUserControl.Instance.LabelTimer.Content = GetHours() + ":" + GetMinutes() + ":" + GetSeconds();
            });
        }

        private string GetSeconds()
        {
            return _stopWatch.Elapsed.Seconds < 10 ? "0" + _stopWatch.Elapsed.Seconds.ToString() : _stopWatch.Elapsed.Seconds.ToString();
        }

        private string GetMinutes()
        {
            return _stopWatch.Elapsed.Minutes < 10 ? "0" + _stopWatch.Elapsed.Minutes.ToString() : _stopWatch.Elapsed.Minutes.ToString();
        }

        private string GetHours()
        {
            return _stopWatch.Elapsed.Hours < 10 ? "0" + _stopWatch.Elapsed.Hours.ToString() : _stopWatch.Elapsed.Hours.ToString();
        }
    }
}
