using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStack
{
    

    public enum ThumbStickType
    {
        LeftHorizontal,
        LeftVertical,
        RightHorizontal,
        RightVertical,
        Left,
        Right,
    }

    Stack<InputPad> _inputPadStack = new Stack<InputPad>();
    // Start is called before the first frame update
    public void Awake()
    {
        _inputPadStack.Push(new InputPad());
    }

    // Update is called once per frame
    public void Update()
    {
        //InputPadの現在状態をprevに保存
        foreach (var item in _inputPadStack)
        {
            //InputPadの現在状態をprevに保存
            item.Backup();
            //InputPadの現在状態をクリア
            item.Clear();

            //InputPadにInputから収集した情報を設定
            item.HorizontalLeft = Input.GetAxis("HorizontalLeft");
            item.VerticalLeft = Input.GetAxis("VerticalLeft");
            item.HorizontalRight = Input.GetAxis("HorizontalRight");
            item.VerticalRight = Input.GetAxis("HorizontalRight");

            item.ButtonLeft = Input.GetButton("ButtonLeft");
            item.ButtonUp = Input.GetButton("ButtonUp");
            item.ButtonRight = Input.GetButton("ButtonRight");
            item.ButtonDown = Input.GetButton("ButtonDown");

            item.KeyLeft = Input.GetKey("KeyLeft");
            item.KeyUp = Input.GetKey("KeyUp");
            item.KeyRight = Input.GetKey("KeyRight");
            item.KeyDown = Input.GetKey("KeyDown");

            item.L1 = Input.GetButton("L1");
            item.R1 = Input.GetButton("R1");
            item.L2 = (Input.GetAxis("L2") + 1.0f) / 2.0f;  //-1.0f~1.0fの範囲を0.0f~1.0fに変換
            item.R2 = (Input.GetAxis("R2") + 1.0f) / 2.0f;  //-1.0f~1.0fの範囲を0.0f~1.0fに変換
            item.L3 = Input.GetButton("L3");
            item.R3 = Input.GetButton("R3");
        }
    }

}
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

public class InputManager : MonoBehaviour
{

    Stack<InputPad> _inputPadStack = new Stack<InputPad>();
    float _triggerDelta = 0.5f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _inputPadStack.Push(new InputPad());
    }

    public void Update()
    {
        //InputPadの現在状態をprevに保存
        foreach (var item in _inputPadStack)
        {
            //InputPadの現在状態をprevに保存
            item.Backup();
            //InputPadの現在状態をクリア
            item.Clear();

            //InputPadにInputから収集した情報を設定
            item.HorizontalLeft = Input.GetAxis("HorizontalLeft");
            item.VerticalLeft = Input.GetAxis("VerticalLeft");
            item.HorizontalRight = Input.GetAxis("HorizontalRight");
            item.VerticalRight = Input.GetAxis("VerticalRight");

            item.ButtonLeft = Input.GetButton("ButtonLeft");
            item.ButtonUp = Input.GetButton("ButtonUp");
            item.ButtonRight = Input.GetButton("ButtonRight");
            item.ButtonDown = Input.GetButton("ButtonDown");

            item.KeyLeft = Input.GetAxis("HorizontalKey") < 0.0f;
            item.KeyUp = Input.GetAxis("VerticalKey") > 0.0f;
            item.KeyRight = Input.GetAxis("HorizontalKey") > 0.0f;
            item.KeyDown = Input.GetAxis("VerticalKey") < 0.0f;

            item.L1 = Input.GetButton("L1");
            item.R1 = Input.GetButton("R1");
            item.L2 = (Input.GetAxis("L2") + 1.0f) / 2.0f;  //-1.0f~1.0fの範囲を0.0f~1.0fに変換
            item.R2 = (Input.GetAxis("R2") + 1.0f) / 2.0f;  //-1.0f~1.0fの範囲を0.0f~1.0fに変換
            item.L3 = Input.GetButton("L3");
            item.R3 = Input.GetButton("R3");
        }

        DebugPrint();
    }

    public bool IsTrigger(InputType.Button button)
    {
        bool ret = false;
        var peek = _inputPadStack.Peek();
        switch (button)
        {
            case InputType.Button.Left:
                ret = peek.ButtonLeft;
                break;
            case InputType.Button.Up:
                ret = peek.ButtonUp;
                break;
            case InputType.Button.Right:
                ret = peek.ButtonRight;
                break;
            case InputType.Button.Down:
                ret = peek.ButtonDown;
                break;
            case InputType.Button.KeyLeft:
                ret = peek.KeyLeft;
                break;
            case InputType.Button.KeyUp:
                ret = peek.KeyUp;
                break;
            case InputType.Button.KeyRight:
                ret = peek.KeyRight;
                break;
            case InputType.Button.KeyDown:
                ret = peek.KeyDown;
                break;
            case InputType.Button.L1:
                ret = peek.L1;
                break;
            case InputType.Button.R1:
                ret = peek.R1;
                break;
            case InputType.Button.L2:
                ret = peek.L2 > _triggerDelta;
                break;
            case InputType.Button.R2:
                ret = peek.R2 > _triggerDelta;
                break;
            case InputType.Button.L3:
                ret = peek.L3;
                break;
            case InputType.Button.R3:
                ret = peek.R3;
                break;
            case InputType.Button.Num:
                break;
            default:
                break;
        }
        return ret;
    }

    public float GetAnalog(InputType.Analog Analog)
    {
        float ret = 0.0f;
        var peek = _inputPadStack.Peek();
        switch (Analog)
        {
            case InputType.Analog.HorizontalLeft:
                ret = peek.HorizontalLeft;
                break;
            case InputType.Analog.VerticalLeft:
                ret = peek.VerticalLeft;
                break;
            case InputType.Analog.HorizontalRight:
                ret = peek.HorizontalRight;
                break;
            case InputType.Analog.VerticalRight:
                ret = peek.VerticalRight;
                break;
            case InputType.Analog.L2:
                ret = peek.L2;
                break;
            case InputType.Analog.R2:
                ret = peek.R2;
                break;
            default:
                break;
        }
        return ret;
    }

    public Vector2 GetThumbStick(InputType.ThumbStick thumbStick)
    {
        Vector2 ret = Vector2.zero;
        var peek = _inputPadStack.Peek();
        switch (thumbStick)
        {
            case InputType.ThumbStick.Left:
                ret.x = peek.HorizontalLeft;
                ret.y = peek.VerticalLeft;
                break;
            case InputType.ThumbStick.Right:
                ret.x = peek.HorizontalRight;
                ret.y = peek.VerticalRight;
                break;
            default:
                break;
        }
        return ret;
    }

    public void DebugPrint()
    {
        var peek = _inputPadStack.Peek();
        peek.DebugPrint();
    }
}