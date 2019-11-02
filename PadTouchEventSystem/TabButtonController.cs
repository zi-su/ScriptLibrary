using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パッド操作で選択しないボタン制御クラス
/// </summary>
public class TabButtonController : MonoBehaviour
{
    List<ButtonBase> _tabButtons = new List<ButtonBase>();
    int _index = 0;

    public void Next()
    {
        _tabButtons[_index].Exit();
        _index++;
        if(_index > _tabButtons.Count - 1)
        {
            _index = _tabButtons.Count - 1;
        }
        _tabButtons[_index].Enter();
    }

    public void Prev()
    {
        _tabButtons[_index].Exit();
        _index--;
        if(_index < 0) { _index = 0; }
        _tabButtons[_index].Enter();
    }
}
