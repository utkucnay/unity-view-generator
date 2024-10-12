using System;
using System.Collections.Generic;
using TMPro;

class DropdownEventArgs : EventArgs
{
    public int Index { get; private set; }
    public TMP_Dropdown.OptionData[] Options { get; private set; }
    public TMP_Dropdown.OptionData CurrentOptions { get { return Options[Index]; } }
    
    public DropdownEventArgs(int index, List<TMP_Dropdown.OptionData> options)
    {
        Index = index;
        Options = options.ToArray();
    }
}