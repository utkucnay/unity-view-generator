using System;
using UnityEngine;

namespace ViewGenerator
{
    public class ScrollViewEventArgs : EventArgs 
    {
        public Vector2 Value { get; set; }

        public ScrollViewEventArgs(Vector2 value)
        {
            Value = value;
        }
    }
}