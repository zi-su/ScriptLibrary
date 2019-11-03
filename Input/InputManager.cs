using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class InputManager : SingletonMonobehaviour<InputManager>
{
    List<InputPad> _inputPadList = new List<InputPad>();
    float _triggerDelta = 0.5f;
    float _repeatWait = 0.0f;
    const float RepeatStartWait = 1.0f;
    const float RepeatingWait = 0.05f;
    
    enum RepeatState
    {
        Stop,
        Start,
        Repeat,
    }
    RepeatState _repeatState = RepeatState.Stop;

    protected override void Awake()
    {
        base.Awake();
        gameObject.name = "InputManager";
    }

    public InputPad CreateInputPad()
    {
        InputPad pad = new InputPad();
        if(_inputPadList.Count > 1)
        {
            var prev = _inputPadList[_inputPadList.Count - 1];
            prev.IsEnable = false;
        }
        _inputPadList.Add(pad);
        return pad;
    }

    public void Remove(InputPad pad)
    {
        if(_inputPadList.Count > 0)
        {
            _inputPadList.Remove(pad);
            if(_inputPadList.Count > 0)
            {
                _inputPadList[_inputPadList.Count - 1].IsEnable = true;
            }
        }
    }

    public InputPad CurrentPad()
    {
        if(_inputPadList.Count > 0)
        {
            return _inputPadList[_inputPadList.Count - 1];
        }
        return null;
    }

    public void Update()
    {
        //InputPadの現在状態をprevに保存
        foreach (var item in _inputPadList)
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
    }

    public void DebugPrint()
    {
        var peek = _inputPadList[_inputPadList.Count - 1];
        peek.DebugPrint();
    }
}