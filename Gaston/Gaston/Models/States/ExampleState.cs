using System;
using System.Xml.Schema;

namespace Gaston.Models.States
{
    public class ExampleState
    {
        private bool _completed;

        public delegate void ExampleCompletedEventHandler(object source, EventArgs args);
        public event ExampleCompletedEventHandler ExampleCompleted;
        public int Score { get; set; }

        public bool Completed
        {
            get => _completed;
            set
            {
                if (value)
                { 
                    _completed = true;
                    OnExampleCompleted(this);
                }
            }
        }


        protected virtual void OnExampleCompleted(object source)
        {
            ExampleCompleted?.Invoke(source, EventArgs.Empty);
        }
    }
}