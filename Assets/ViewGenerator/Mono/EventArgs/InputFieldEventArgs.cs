using System;

namespace ViewGenerator
{
    public class InputFieldEventArgs : EventArgs
    {
        public string Value { get; set; }

        public InputFieldEventArgs(string value)
        {
            Value = value;
        }
    }
}