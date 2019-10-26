using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class CommonButtonHandler : ButtonHandlerBase
{
    [SerializeField]
    Image _selectImage = null;
    public delegate void onClick();
    
    public onClick OnClick
    {
        get;set;
    }

    public override void Enter()
    {
       _selectImage.enabled = true;
    }

    public override void Exit()
    {
        _selectImage.enabled = false;
    }

    public override void Click()
    {
        OnClick();
    }
    public override void Disable()
    {
    }
}
