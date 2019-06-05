using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStack
{
    public enum ButtonType
    {
        Button0,    //□
        Button1,    //△
        Button2,    //○
        Button3,    //☓

        Button4,    //←
        Button5,    //↑
        Button6,    //→
        Button7,    //↓

        L1,
        R1,
        L2,
        R2,

        L3,
        R3,
        Num,
    }

    public enum ThumbStickType
    {
        LeftHorizontal,
        LeftVertical,
        RightHorizontal,
        RightVertical,
        Left,
        Right,
    }

    public class InputData
    {
        internal float _horizontal = 0.0f;
        internal float _vertical = 0.0f;
        internal float _horizontal2 = 0.0f;
        internal float _vertical2 = 0.0f;

        internal bool _button0 = false;
        internal bool _button1 = false;
        internal bool _button2 = false;
        internal bool _button3 = false;

        internal bool _button4 = false;
        internal bool _button5 = false;
        internal bool _button6 = false;
        internal bool _button7 = false;

        internal bool _l1 = false;
        internal bool _r1 = false;
        internal float _l2 = 0.0f;
        internal float _r2 = 0.0f;
        internal float _l2_prev = 0.0f;
        internal float _r2_prev = 0.0f;
        internal bool _l3 = false;
        internal bool _r3 = false;

        internal bool _triggerButton0 = false;
        internal bool _triggerButton1 = false;
        internal bool _triggerButton2 = false;
        internal bool _triggerButton3 = false;
        internal bool _triggerButton4 = false;
        internal bool _triggerButton5 = false;
        internal bool _triggerButton6 = false;
        internal bool _triggerButton7 = false;

        internal bool _triggerL1 = false;
        internal bool _triggerR1 = false;
        internal bool _triggerL3 = false;
        internal bool _triggerR3 = false;


        public void Clear()
        {
            _horizontal = 0.0f;
            _vertical = 0.0f;
            _horizontal2 = 0.0f;
            _vertical2 = 0.0f;

            _button0 = false;
            _button1 = false;
            _button2 = false;
            _button3 = false;
            _l1 = false;
            _r1 = false;
            _l2_prev = _l2;
            _l2 = 0.0f;
            _r2_prev = _r2;
            _r2 = 0.0f;
            _l3 = false;
            _r3 = false;

            _triggerButton0 = false;
            _triggerButton1 = false;
            _triggerButton2 = false;
            _triggerButton3 = false;
            _triggerL1 = false;
            _triggerR1 = false;
            _triggerL3 = false;
            _triggerR3 = false;
        }

        public void DebugPrint()
        {
            Debug.Log("Horizontal:"+_horizontal);
            Debug.Log("Vertical:" + _vertical);
            Debug.Log("Horizontal2:" + _horizontal2);
            Debug.Log("Vertical2:" + _vertical2);
            Debug.Log("Button0:" + _button0);
            Debug.Log("Button1:" + _button1);
            Debug.Log("Button2:"+_button2);
            Debug.Log("Button3:" + _button3);

            Debug.Log("TriggerButton0:" + _triggerButton0);
            Debug.Log("TriggerButton1:" + _triggerButton1);
            Debug.Log("TriggerButton2:" + _triggerButton2);
            Debug.Log("TriggerButton3:" + _triggerButton3);

            Debug.Log("L1:"+_l1);
            Debug.Log("R1:"+_r1);
            Debug.Log("L2:"+_l2);
            Debug.Log("R2:"+_r2);
            Debug.Log("L3:"+_l3);
            Debug.Log("R3:"+_r3);
        }

        public bool IsTriggerButton(ButtonType bt)
        {
            bool ret = false;
            switch (bt)
            {
                case ButtonType.Button0:
                    ret = _triggerButton0;
                    break;
                case ButtonType.Button1:
                    ret = _triggerButton1;
                    break;
                case ButtonType.Button2:
                    ret = _triggerButton2;
                    break;
                case ButtonType.Button3:
                    ret = _triggerButton3;
                    break;
                case ButtonType.Button4:
                    ret = _triggerButton4;
                    break;
                case ButtonType.Button5:
                    ret = _triggerButton5;
                    break;
                case ButtonType.Button6:
                    ret = _triggerButton6;
                    break;
                case ButtonType.Button7:
                    ret = _triggerButton7;
                    break;
                case ButtonType.L1:
                    ret = _triggerL1;
                    break;
                case ButtonType.R1:
                    ret = _triggerR1;
                    break;
                case ButtonType.L2:
                    ret = _l2 > 0.5f && _l2_prev < 0.5f;
                    break;
                case ButtonType.R2:
                    ret = _r2 > 0.5f && _r2_prev < 0.5f;
                    break;
                case ButtonType.L3:
                    ret = _triggerL3;
                    break;
                case ButtonType.R3:
                    ret = _triggerR3;
                    break;
                case ButtonType.Num:
                    break;
                default:
                    break;
            }
            return ret;
        }
        public bool IsHoldButton(ButtonType bt)
        {
            bool ret = false;
            switch (bt)
            {
                case ButtonType.Button0:
                    ret = _button0;
                    break;
                case ButtonType.Button1:
                    ret = _button1;
                    break;
                case ButtonType.Button2:
                    ret = _button2;
                    break;
                case ButtonType.Button3:
                    ret = _button3;
                    break;
                case ButtonType.Button4:
                    ret = _button4;
                    break;
                case ButtonType.Button5:
                    ret = _button5;
                    break;
                case ButtonType.Button6:
                    ret = _button6;
                    break;
                case ButtonType.Button7:
                    ret = _button7;
                    break;
                case ButtonType.L1:
                    ret = _l1;
                    break;
                case ButtonType.R1:
                    ret = _r1;
                    break;
                case ButtonType.L2:
                    ret = _l2 > 0.5f;
                    break;
                case ButtonType.R2:
                    ret = _r2 > 0.5f;
                    break;
                case ButtonType.L3:
                    ret = _l3;
                    break;
                case ButtonType.R3:
                    ret = _r3;
                    break;
                case ButtonType.Num:
                    break;
                default:
                    break;
            }
            return ret;
        }

        public float GetThumbStick(ThumbStickType st)
        {
            float v = 0.0f;
            switch (st)
            {
                case ThumbStickType.LeftHorizontal:
                    v = _horizontal;
                    break;
                case ThumbStickType.LeftVertical:
                    v = _vertical;
                    break;
                case ThumbStickType.RightHorizontal:
                    v = _horizontal2;
                    break;
                case ThumbStickType.RightVertical:
                    v = _vertical2;
                    break;
                default:
                    break;
            }
            return v;
        }

        public Vector3 GetThumbStickV(ThumbStickType st)
        {
            Vector3 v = new Vector3(0.0f, 0.0f, 0.0f);
            switch (st)
            {
                case ThumbStickType.Left:
                    v.x = _horizontal;
                    v.z = _vertical;
                    break;
                case ThumbStickType.Right:
                    v.x = _horizontal2;
                    v.z = _vertical2;
                    break;
                default:
                    break;
            }
            return v;
        }
    }

    Stack<InputData> _inputDataStack = new Stack<InputData>();

    // Start is called before the first frame update
    public void Awake()
    {
        _inputDataStack.Push(new InputData());
    }

    // Update is called once per frame
    public void Update()
    {
        if (_inputDataStack.Count == 0) return;
        var data = _inputDataStack.Peek();
        foreach (var i in _inputDataStack)
        {
            i.Clear();
        }

        data._horizontal = Input.GetAxis("Horizontal");
        data._vertical = Input.GetAxis("Vertical");
        data._horizontal2 = Input.GetAxis("Horizontal2");
        data._vertical2= Input.GetAxis("Vertical2");

        data._button0 = Input.GetButton("button0");
        data._button1 = Input.GetButton("button1");
        data._button2 = Input.GetButton("button2");
        data._button3 = Input.GetButton("button3");
        data._triggerButton0 = Input.GetButtonDown("button0");
        data._triggerButton1 = Input.GetButtonDown("button1");
        data._triggerButton2 = Input.GetButtonDown("button2");
        data._triggerButton3 = Input.GetButtonDown("button3");
        data._l1 = Input.GetButton("L1");
        data._r1 = Input.GetButton("R1");
        data._l2 = (Input.GetAxis("L2") + 1.0f)/2.0f;
        data._r2 = (Input.GetAxis("R2") + 1.0f)/2.0f;
        data._l3 = Input.GetButton("L3");
        data._r3 = Input.GetButton("R3");

        data._triggerL1 = Input.GetButtonDown("L1");
        data._triggerR1 = Input.GetButtonDown("R1");
        data._triggerL3 = Input.GetButtonDown("L3");
        data._triggerR3 = Input.GetButtonDown("R3");

        //data.DebugPrint();
    }

    public InputData Push()
    {
        var data = new InputData();
        _inputDataStack.Push(data);
        return data;
    }

    public void Pop()
    {
        _inputDataStack.Pop();
    }

    public InputData Peek()
    {
        if(_inputDataStack.Count == 0) { return null; }
        return _inputDataStack.Peek();
    }
}

public class InputManager : MonoBehaviour
{
    static InputStack _inputStack = null;
    static public InputStack Instance
    {
        get
        {
            if(_inputStack == null)
            {
                _inputStack = new InputStack();
            }
            return _inputStack;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance.Awake();
    }

    private void Update()
    {
        Instance.Update();
    }
}