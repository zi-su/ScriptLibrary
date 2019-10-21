using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public abstract class EnterHandlerBase : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    protected Image _raycastImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //現在選択しているものを選択解除呼び出し
        var cgo = CustomEventSystem.CurrentSelected;
        if(cgo != null)
        {
            var handler = cgo.GetComponent<EnterHandlerBase>();
            if(handler != null)
            {
                handler.Exit();
            }
        }

        //現在選択を上書きして選択呼び出し
        Enter();
        CustomEventSystem.CurrentSelected = gameObject;
    }

    /// <summary>
    /// 選択したときの処理
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// 選択解除されたときの処理
    /// </summary>
    public abstract void Exit();

    public abstract void Disable();
}
