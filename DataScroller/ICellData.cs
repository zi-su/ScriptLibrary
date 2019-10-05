using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellData
{
    Vector2 GetCellSize();
    void SetData(ICellData cellData);
}
