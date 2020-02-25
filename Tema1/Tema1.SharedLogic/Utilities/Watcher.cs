using System;
using System.Diagnostics;

namespace Tema1.Core.Utilities
{
    public class Watcher
    {
        private readonly Stopwatch _stopwatch;
        private int _errorCounter = 0;
        public TimeSpan ElapsedTimePerAction { private set; get; }
        public TimeSpan TotalElapsedTime { private set; get; }
        public Watcher() => _stopwatch = new Stopwatch();

        public void MeasureElapsedTime(Action action)
        {
            _stopwatch.Start();
            action.Invoke();
            _stopwatch.Stop();
            TotalElapsedTime = TotalElapsedTime.Add(_stopwatch.Elapsed);
            ElapsedTimePerAction = _stopwatch.Elapsed;
            _stopwatch.Reset();
        }

        public void NewErrorAdded() => _errorCounter++;
        public int NumberOfErrors => _errorCounter;
    }
}