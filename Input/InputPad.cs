using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Managerから入力情報を受け取って、保存状態を更新
/// </summary>
public class InputPad
{
    internal class Data
    {
        //全プラットフォーム共通ボタン定義
        public float HorizontalLeft { get; set; }
        public float VerticalLeft { get; set; }
        public float HorizontalRight { get; set; }
        public float VerticalRight { get; set; }

        public bool ButtonLeft { get; set; }
        public bool ButtonUp { get; set; }
        public bool ButtonRight { get; set; }
        public bool ButtonDown { get; set; }

        public bool KeyLeft { get; set; }
        public bool KeyUp { get; set; }
        public bool KeyRight { get; set; }
        public bool KeyDown { get; set; }

        public bool L1 { get; set; }
        public bool R1 { get; set; }

        public float L2 { get; set; }
        public float R2 { get; set; }

        public bool L3 { get; set; }
        public bool R3 { get; set; }
    }
    

    //前回入力状態
    Data _prev = new Data();
    Data now = new Data();

    //全プラットフォーム共通ボタン定義
    public float HorizontalLeft { get { return now.HorizontalLeft; } set { now.HorizontalLeft = value; } }
    public float VerticalLeft { get { return now.VerticalLeft; } set { now.VerticalLeft = value; } }
    public float HorizontalRight { get { return now.HorizontalRight; } set { now.HorizontalRight = value; } }
    public float VerticalRight { get { return now.VerticalRight; } set { now.VerticalRight = value; } }

    public bool ButtonLeft { get { return now.ButtonLeft; } set { now.ButtonLeft = value; } }
    public bool ButtonUp { get { return now.ButtonUp; } set { now.ButtonUp = value; } }
    public bool ButtonRight { get { return now.ButtonRight; } set { now.ButtonRight = value; } }
    public bool ButtonDown { get { return now.ButtonDown; } set { now.ButtonDown = value; } }

    public bool KeyLeft { get { return now.KeyLeft; } set { now.KeyLeft = value; } }
    public bool KeyUp { get { return now.KeyUp; } set { now.KeyUp = value; } }
    public bool KeyRight { get { return now.KeyRight; } set { now.KeyRight = value; } }
    public bool KeyDown { get { return now.KeyDown; } set { now.KeyDown = value; } }

    public bool L1 { get { return now.L1; } set { now.L1 = value; } }
    public bool R1 { get { return now.R1; } set { now.R1 = value; } }

    public float L2 { get { return now.L2; } set { now.L2 = value; } }
    public float R2 { get { return now.R2; } set { now.R2 = value; } }

    public bool L3 { get { return now.L3; } set { now.L3 = value; } }
    public bool R3 { get { return now.R3; } set { now.R3 = value; } }

    /// <summary>
    /// 前回情報に保存
    /// </summary>
    public void Backup()
    {
        _prev.HorizontalLeft = now.HorizontalLeft;
        _prev.VerticalLeft = now.VerticalLeft;
        _prev.HorizontalRight = now.HorizontalRight;
        _prev.VerticalRight = now.VerticalRight;

        _prev.ButtonLeft = now.ButtonLeft;
        _prev.ButtonUp = now.ButtonUp;
        _prev.ButtonRight = now.ButtonRight;
        _prev.ButtonDown = now.ButtonDown;

        _prev.KeyLeft = now.KeyLeft;
        _prev.KeyUp = now.KeyUp;
        _prev.KeyRight = now.KeyRight;
        _prev.KeyDown= now.KeyDown;

        _prev.L1 = now.L1;
        _prev.R1 = now.R1;
        _prev.L2 = now.L2;
        _prev.R2 = now.R2;

        _prev.L3 = now.L3;
        _prev.R3 = now.R3;
    }

    public void Clear()
    {
        now.HorizontalLeft = 0.0f;
        now.VerticalLeft = 0.0f;
        now.HorizontalRight = 0.0f;
        now.VerticalRight = 0.0f;

        now.ButtonLeft = false;
        now.ButtonRight = false;
        now.ButtonUp = false;
        now.ButtonDown = false;

        now.KeyLeft = false;
        now.KeyUp = false;
        now.KeyRight = false;
        now.KeyDown = false;

        now.L1 = false;
        now.R1 = false;
        now.L2 = 0.0f;
        now.R2 = 0.0f;
        now.L3 = false;
        now.R3 = false;
    }

    public void DebugPrint()
    {
        //Debug.Log("HorizontalLeft " + now.HorizontalLeft);
        //Debug.Log("VerticalLeft " + now.VerticalLeft);
        //Debug.Log("HorizontalRight " + now.HorizontalRight);
        //Debug.Log("VerticalRight " + now.VerticalRight);

        //Debug.Log("ButtonLeft " + now.ButtonLeft);
        //Debug.Log("ButtonUp " + now.ButtonUp);
        //Debug.Log("ButtonRight " + now.ButtonRight);
        //Debug.Log("ButtonDown " + now.ButtonDown);
        //
        //Debug.Log("KeyLeft " + now.KeyLeft);
        //Debug.Log("KeyUp " + now.KeyUp);
        //Debug.Log("KeyRight " + now.KeyRight);
        //Debug.Log("KeyDown " + now.KeyDown);
        //
        //Debug.Log("L1 " + now.L1);
        //Debug.Log("R1 " + now.R1);
        //Debug.Log("L2 " + now.L2);
        //Debug.Log("R2 " + now.R2);
        //
        //Debug.Log("L3 " + now.L3);
        //Debug.Log("R3 " + now.R3);

    }
    //プラットフォーム固有ボタンのためのクラス
}
