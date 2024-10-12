using System;

public class ScrollbarEventArgs : EventArgs
{
    public float Value { get; private set; }

    public ScrollbarEventArgs(float value)
    {
        Value = value;
    }
}