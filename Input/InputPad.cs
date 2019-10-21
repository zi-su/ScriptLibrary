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
    Data prev = new Data();
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

    //
    public float PrevHorizontalLeft { get { return prev.HorizontalLeft; } set { prev.HorizontalLeft = value; } }
    public float PrevVerticalLeft { get { return prev.VerticalLeft; } set { prev.VerticalLeft = value; } }
    public float PrevHorizontalRight { get { return prev.HorizontalRight; } set { prev.HorizontalRight = value; } }
    public float PrevVerticalRight { get { return prev.VerticalRight; } set { prev.VerticalRight = value; } }

    public bool PrevButtonLeft { get { return prev.ButtonLeft; } set { prev.ButtonLeft = value; } }
    public bool PrevButtonUp { get { return prev.ButtonUp; } set { prev.ButtonUp = value; } }
    public bool PrevButtonRight { get { return prev.ButtonRight; } set { prev.ButtonRight = value; } }
    public bool PrevButtonDown { get { return prev.ButtonDown; } set { prev.ButtonDown = value; } }

    public bool PrevKeyLeft { get { return prev.KeyLeft; } set { prev.KeyLeft = value; } }
    public bool PrevKeyUp { get { return prev.KeyUp; } set { prev.KeyUp = value; } }
    public bool PrevKeyRight { get { return prev.KeyRight; } set { prev.KeyRight = value; } }
    public bool PrevKeyDown { get { return prev.KeyDown; } set { prev.KeyDown = value; } }

    public bool PrevL1 { get { return prev.L1; } set { prev.L1 = value; } }
    public bool PrevR1 { get { return prev.R1; } set { prev.R1 = value; } }

    public float PrevL2 { get { return prev.L2; } set { prev.L2 = value; } }
    public float PrevR2 { get { return prev.R2; } set { prev.R2 = value; } }

    public bool PrevL3 { get { return prev.L3; } set { prev.L3 = value; } }
    public bool PrevR3 { get { return prev.R3; } set { prev.R3 = value; } }

    /// <summary>
    /// 前回情報に保存
    /// </summary>
    public void Backup()
    {
        prev.HorizontalLeft = now.HorizontalLeft;
        prev.VerticalLeft = now.VerticalLeft;
        prev.HorizontalRight = now.HorizontalRight;
        prev.VerticalRight = now.VerticalRight;

        prev.ButtonLeft = now.ButtonLeft;
        prev.ButtonUp = now.ButtonUp;
        prev.ButtonRight = now.ButtonRight;
        prev.ButtonDown = now.ButtonDown;

        prev.KeyLeft = now.KeyLeft;
        prev.KeyUp = now.KeyUp;
        prev.KeyRight = now.KeyRight;
        prev.KeyDown= now.KeyDown;

        prev.L1 = now.L1;
        prev.R1 = now.R1;
        prev.L2 = now.L2;
        prev.R2 = now.R2;

        prev.L3 = now.L3;
        prev.R3 = now.R3;
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
