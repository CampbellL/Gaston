using System;
using System.Xml.Schema;

namespace Gaston.Models.States
{
    public class ExampleState
    {
        public bool Completed
        {
            set
            {
                if (value)
                {
                    OnExampleCompleted(this);
                    Completed = true;
                }
            }
        }

        public int Score { get; set; }
        
        public delegate void ExampleCompletedEventHandler(object source, EventArgs args);
        public event ExampleCompletedEventHandler ExampleCompleted;
        
        
        
        protected virtual void OnExampleCompleted(object source)
        {
            ExampleCompleted?.Invoke(source, EventArgs.Empty);
        }
    }
}