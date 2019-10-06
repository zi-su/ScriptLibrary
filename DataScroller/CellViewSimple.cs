using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スクロール時に1要素の表示更新をするためのクラス
/// </summary>
public class CellViewSimple : MonoBehaviour, ICellView
{
    public void UpdateView(ICellData cellData)
    {
        var data = cellData as CellDataBase;
        var rect = transform as RectTransform;
        rect.sizeDelta = data.GetCellSize();
    }
}
