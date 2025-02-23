using System;

namespace ViewGenerator
{
    public class SliderEventArgs : EventArgs
    {
        public float Value { get; private set; }

        public SliderEventArgs(float value)
        {
            Value = value;
        }
    }
}