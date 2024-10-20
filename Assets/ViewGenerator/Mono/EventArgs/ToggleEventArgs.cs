using System;

namespace ViewGenerator
{
    public class ToggleEventArgs : EventArgs
    {
        public bool IsChecked { get; private set; }

        public ToggleEventArgs(bool isChecked)
        {
            IsChecked = isChecked;
        }
    }
}