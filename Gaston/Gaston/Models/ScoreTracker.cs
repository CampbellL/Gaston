using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Schema;
using Xamarin.Forms;

namespace Gaston.Models
{
    public class ScoreTracker
    {
        
        public delegate void TimeReachedZeroHandler(object source,EventArgs args);
        
        public event TimeReachedZeroHandler OutOfTime;
        
        private int _seconds;
        private readonly int _totalScore;
        private readonly int _minScore;
        private readonly int _penalty;
        private TimeSpan _span;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public ScoreTracker(int seconds, int totalScore, int minScore, int penalty)
        {
            _seconds = seconds;
            _totalScore = totalScore;
            _minScore = minScore;
            this._penalty = penalty;
            Device.StartTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                _stopwatch.Start();
                _span = _stopwatch.Elapsed;
                return false;
            });
            
        }

        public int GetElapsedSeconds()
        {
            if (_stopwatch.IsRunning)
            {
                var otest = _span.TotalSeconds;
                return Convert.ToInt32(_span.TotalSeconds);
            }

            return 0;

        }

        public int GetScore()
        {
            var test = GetElapsedSeconds();
            int score = _totalScore - (_penalty * GetElapsedSeconds());
            if (score > _minScore)
                return score;
            return _minScore;
        }
        
        protected virtual void OnOutOfTime(object source)
        {
            OutOfTime?.Invoke(source, EventArgs.Empty);
        }
    }
}