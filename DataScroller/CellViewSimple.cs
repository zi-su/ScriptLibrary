using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CellViewSimple : MonoBehaviour, ICellView
{
    public void UpdateView(ICellData cellData)
    {
        var data = cellData as CellDataBase;
        var rect = transform as RectTransform;
        rect.sizeDelta = data.GetCellSize();
    }
}
