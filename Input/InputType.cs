using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputType
{
    public enum Button
    {
        Left,    //□
        Up,    //△
        Right,    //○
        Down,    //☓

        KeyLeft,    //←
        KeyUp,    //↑
        KeyRight,    //→
        KeyDown,    //↓

        L1,
        R1,
        L2,
        R2,

        L3,
        R3,
        Num,
    }

    public enum Analog
    {
        HorizontalLeft,
        VerticalLeft,
        HorizontalRight,
        VerticalRight,
        L2,
        R2,
    }

    public enum ThumbStick
    {
        Left,
        Right,
    }
}
