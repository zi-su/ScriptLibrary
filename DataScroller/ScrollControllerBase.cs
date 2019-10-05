using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// データリストをもとにしてスクロール内の要素(セル）を
/// 使いまわして高速なスクロールを行うためのコントローラクラス
/// </summary>
public class ScrollControllerBase : MonoBehaviour
{
    enum Mode
    {
        Horizontal,
        Vertical,
    }

    [SerializeField]
    Mode _mode;

    [SerializeField]
    GameObject _cellPrefab;
    [SerializeField]
    RectTransform _scrollRect;
    [SerializeField]
    RectTransform _contentRect;

    List<ICellData> _cellDataList = new List<ICellData>();
    LinkedList<GameObject> _cellLinkList = new LinkedList<GameObject>();
    int _cellNum = 0;

    int _cellDataIndex = 0;
    int _cellViewIndex = 0;

    float _diffMove;
    // Start is called before the first frame update
    void Start()
    {
        AddData(new CellDataBase(100.0f, 100.0f));
        AddData(new CellDataBase(100.0f, 200.0f));
        AddData(new CellDataBase(100.0f, 100.0f));
        AddData(new CellDataBase(100.0f, 200.0f));
        AddData(new CellDataBase(100.0f, 100.0f));
        AddData(new CellDataBase(100.0f, 100.0f));
        AddData(new CellDataBase(100.0f, 100.0f));
        AddData(new CellDataBase(100.0f, 300.0f));
        AddData(new CellDataBase(100.0f, 200.0f));
        AddData(new CellDataBase(100.0f, 100.0f));
        AddData(new CellDataBase(100.0f, 100.0f));
        AddData(new CellDataBase(100.0f, 100.0f));

        CalcContentSize();
        _cellNum = CalcCellNum();
        InstantiateCell();
    }

    // Update is called once per frame
    void Update()
    {
        ScrollDown();
        ScrollUp();
    }

    void ScrollDown()
    {
        while (_contentRect.anchoredPosition.y + _diffMove > _cellDataList[_cellDataIndex].GetCellSize().y)
        {
            if (_contentRect.anchoredPosition.y > _contentRect.sizeDelta.y - _scrollRect.sizeDelta.y) { return; }
            if (_cellDataIndex + _cellNum > _cellDataList.Count) { return; }
            _diffMove -= _cellDataList[_cellDataIndex].GetCellSize().y;

            MoveLast();

            var first = _cellLinkList.First;
            _cellLinkList.RemoveFirst();
            _cellLinkList.AddLast(first);
            _cellDataIndex++;
        }
    }

    void ScrollUp()
    {
        while (_contentRect.anchoredPosition.y + _diffMove < 0.0f)
        {
            if (_contentRect.anchoredPosition.y < 0.0f) { return; }
            _cellDataIndex--;
            _diffMove += _cellDataList[_cellDataIndex].GetCellSize().y;

            MoveFirst();

            var last = _cellLinkList.Last;
            _cellLinkList.RemoveLast();
            _cellLinkList.AddFirst(last);
        }
    }

    public void AddData(ICellData data)
    {
        _cellDataList.Add(data);
    }

    /// <summary>
    /// データリストの各セルサイズからコンテントの幅を計算
    /// </summary>
    void CalcContentSize()
    {
        var size = _contentRect.sizeDelta;
        size.y = 0.0f;
        foreach (var item in _cellDataList)
        {
            size.y += item.GetCellSize().y;
        }
        _contentRect.sizeDelta = size;
    }

    /// <summary>
    /// データリストの最小セルサイズから表示するセルの数を計算
    /// </summary>
    /// <returns></returns>
    int CalcCellNum()
    {
        int num = 0;
        float scrollSize = _scrollRect.sizeDelta.y;
        float minCellSize = int.MaxValue;
        foreach (var item in _cellDataList)
        {
            if(item.GetCellSize().y < minCellSize)
            {
                minCellSize = item.GetCellSize().y;
            }
        }
        for(int i = 0; i < _cellDataList.Count; i++)
        {
            scrollSize -= minCellSize;
            if(scrollSize < 0)
            {
                num++;
                break;
            }
            num++;
        }
        num++;
        return num;
    }

    /// <summary>
    /// セルの生成処理
    /// </summary>
    void InstantiateCell()
    {
        float p = 0;
        for(int i = 0; i < _cellNum; i++)
        {
            var cell = Instantiate(_cellPrefab, _contentRect);
            var rect = cell.transform as RectTransform;
            var pos = rect.anchoredPosition;
            p += i == 0 ? 0 : _cellDataList[i - 1].GetCellSize().y;
            pos.y = -p;
            rect.anchoredPosition = pos;
            var view = cell.GetComponent<ICellView>();
            view.UpdateView(_cellDataList[i]);
            _cellLinkList.AddLast(cell);
        }
    }

    /// <summary>
    /// 末尾の要素を戦闘に移動する計算
    /// </summary>
    void MoveFirst()
    {
        var last = _cellLinkList.Last;
        var lastTrans = last.Value.transform as RectTransform;

        var first = _cellLinkList.First;
        var firstTrans = first.Value.transform as RectTransform;

        var pos = lastTrans.anchoredPosition;
        pos.y = firstTrans.anchoredPosition.y + lastTrans.sizeDelta.y;
        lastTrans.anchoredPosition = pos;
    }

    /// <summary>
    /// 先頭の要素を末尾に移動する計算
    /// </summary>
    void MoveLast()
    {
        var first = _cellLinkList.First;
        var firstTrans = first.Value.transform as RectTransform;
        var last = _cellLinkList.Last;
        var lastTrans = last.Value.transform as RectTransform;
        var pos = firstTrans.anchoredPosition;
        pos.y = lastTrans.anchoredPosition.y - lastTrans.sizeDelta.y;
        firstTrans.anchoredPosition = pos;
    }
}
