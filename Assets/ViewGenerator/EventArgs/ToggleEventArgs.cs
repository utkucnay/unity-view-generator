using System;

public class ToggleEventArgs : EventArgs
{
    public bool IsChecked { get; private set; }

    public ToggleEventArgs(bool isChecked)
    {
        IsChecked = isChecked;
    }
}