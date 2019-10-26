using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public abstract class ButtonHandlerBase : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        //現在選択しているものを選択解除呼び出し
        var cgo = CustomEventSystem.Instance().CurrentSelected;
        if(cgo != null)
        {
            var handler = cgo.GetComponent<ButtonHandlerBase>();
            if(handler != null)
            {
                handler.Exit();
            }
        }

        //現在選択を上書きして選択呼び出し
        Enter();
        CustomEventSystem.Instance().CurrentSelected = gameObject;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    /// <summary>
    /// 選択したときの処理
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// 選択解除されたときの処理
    /// </summary>
    public abstract void Exit();

    /// <summary>
    /// 決定したときの処理
    /// </summary>
    public abstract void Click();

    /// <summary>
    /// 無効化
    /// </summary>
    public abstract void Disable();
}
