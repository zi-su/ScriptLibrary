using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// パッド操作で選択するボタン基底クラス
/// </summary>
public class SelectableButton : ButtonBase, IPointerEnterHandler, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //現在選択しているものを選択解除呼び出し
        var cgo = CustomEventSystem.Instance().CurrentSelected;
        if (cgo != null)
        {
            var handler = cgo.GetComponent<ButtonBase>();
            if (handler != null)
            {
                handler.Exit();
            }
        }

        //選択呼び出し
        Enter();
        //選択オブジェクトを上書き
        CustomEventSystem.Instance().CurrentSelected = gameObject;
    }
}
