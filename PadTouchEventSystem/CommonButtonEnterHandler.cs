using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonButtonEnterHandler : EnterHandlerBase
{
    [SerializeField]
    Image _selectImage;
    [SerializeField]
    Image _disableImage;

    public override void Select()
    {
        _selectImage.enabled = true;
    }

    public override void Deselect()
    {
        _selectImage.enabled = false;
    }

    public override void Decide()
    {
        
    }
    public override void Disable()
    {
        _disableImage.enabled = true;
        _raycastImage.raycastTarget = false;
    }
}
