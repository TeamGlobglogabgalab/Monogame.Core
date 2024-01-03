using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.Structs;

public struct Padding
{
    public int All
    {
        set
        {
            Left = value;
            Top = value;
            Right = value;
            Bottom = value;
        }
    }
    public int Left { get; set; }
    public int Top { get; set; }
    public int Right { get; set; }
    public int Bottom { get; set; }

    public Padding(int padding)
    {
        Left = padding;
        Top = padding;
        Right = padding;
        Bottom = padding;
    }

    public Padding(int left, int top, int right, int bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public static Padding operator +(Padding padding, int value)
    {
        padding.Left += value;
        padding.Top += value;
        padding.Right += value;
        padding.Bottom += value;
        return padding;
    }
    
    public static Padding operator -(Padding padding, int value)
    {
        padding.Left -= value;
        padding.Top -= value;
        padding.Right -= value;
        padding.Bottom -= value;
        return padding;
    }
    
    public static Padding operator *(Padding padding, float value)
    {
        padding.Left = (int)(padding.Left * value);
        padding.Top = (int)(padding.Left * value);
        padding.Right = (int)(padding.Left * value);
        padding.Bottom = (int)(padding.Left * value);
        return padding;
    }
    
    public static Padding operator /(Padding padding, float value)
    {
        padding.Left = (int)(padding.Left / value);
        padding.Top = (int)(padding.Left / value);
        padding.Right = (int)(padding.Left / value);
        padding.Bottom = (int)(padding.Left / value);
        return padding;
    }
}
