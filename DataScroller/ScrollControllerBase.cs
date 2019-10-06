using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// データリストをもとにしてスクロール内の要素(セル）を
/// 使いまわして高速なスクロールを行うためのコントローラクラス
/// Cellプレハブは
/// Verticalの場合、Pivot(0.5f,1.0f)、Anchors(min(0.5,1),max(0.5,1))
/// Horizontalの場合、Pivot(0.0f, 0.5f), Anchors(min(0,0.5f),max(0.0f, 0.5f))
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
    float _space;
    [SerializeField]
    float _marginTop;
    [SerializeField]
    float _marginBottom;

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

    float _diffMove;

    float GetCellSize(int dataIndex)
    {
        return _mode == Mode.Horizontal ? _cellDataList[dataIndex].GetCellSize().x : _cellDataList[dataIndex].GetCellSize().y;
    }

    float GetContentAnchoredPos()
    {
        return _mode == Mode.Horizontal ? -_contentRect.anchoredPosition.x : _contentRect.anchoredPosition.y;
    }

    float GetContentSize()
    {
        return _mode == Mode.Horizontal ? _contentRect.sizeDelta.x : _contentRect.sizeDelta.y;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //テストコード
        TestData();

        CalcContentSize();
        _cellNum = CalcCellNum();
        InstantiateCell();
    }

    void TestData()
    {
        for (int i = 0; i < 100; i++)
        {
            AddData(new CellDataBase(100.0f, 100.0f));
            AddData(new CellDataBase(200.0f, 200.0f));
            AddData(new CellDataBase(100.0f, 100.0f));
            AddData(new CellDataBase(100.0f, 200.0f));
            AddData(new CellDataBase(300.0f, 100.0f));
            AddData(new CellDataBase(100.0f, 100.0f));
            AddData(new CellDataBase(100.0f, 100.0f));
            AddData(new CellDataBase(200.0f, 300.0f));
            AddData(new CellDataBase(100.0f, 200.0f));
            AddData(new CellDataBase(100.0f, 100.0f));
        }
    }
    public void Initialize(List<CellDataBase> dataList)
    {
        _cellDataList.AddRange(dataList);
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
        while (GetContentAnchoredPos() + _diffMove > GetCellSize(_cellDataIndex))
        {
            if (_cellDataIndex + _cellNum >= _cellDataList.Count) { return; }
            _diffMove -= (GetCellSize(_cellDataIndex) + _space);

            
            MoveLast();

            var first = _cellLinkList.First;
            _cellLinkList.RemoveFirst();
            _cellLinkList.AddLast(first);
            _cellDataIndex++;
        }
    }

    void ScrollUp()
    {
        while (GetContentAnchoredPos() + _diffMove < 0.0f)
        {
            if (GetContentAnchoredPos() < 0.0f) { return; }
            
            _cellDataIndex--;
            _diffMove += (GetCellSize(_cellDataIndex) + _space);

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
        float size = 0.0f;
        size += _marginTop;
        for(int i = 0; i < _cellDataList.Count; i++)
        {
            size += GetCellSize(i);
        }
        size += _space * (_cellDataList.Count - 1);
        size += _marginBottom;
        Vector2 sizeDelta = _contentRect.sizeDelta;
        if (_mode == Mode.Horizontal) { sizeDelta.x = size; }
        else { sizeDelta.y = size; }
        _contentRect.sizeDelta = sizeDelta;
    }

    /// <summary>
    /// データリストの最小セルサイズから表示するセルの数を計算
    /// </summary>
    /// <returns></returns>
    int CalcCellNum()
    {
        int num = 0;
        float scrollSize = _mode == Mode.Horizontal ? _scrollRect.sizeDelta.x : _scrollRect.sizeDelta.y;
        float minCellSize = int.MaxValue;
        for (int i = 0; i < _cellDataList.Count; i++)
        {
            if (GetCellSize(i) < minCellSize)
            {
                minCellSize = GetCellSize(i);
            }
        }
        
        for(int i = 0; i < _cellDataList.Count; i++)
        {
            scrollSize -= minCellSize;
            scrollSize -= i == 0 ? 0.0f : _space;
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
            p += i == 0 ?  _marginTop : GetCellSize(i - 1) + _space;
            if (_mode == Mode.Horizontal) { pos.x = p; }
            else { pos.y = -p; }
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
        if (_mode == Mode.Horizontal) { pos.x = firstTrans.anchoredPosition.x - (GetCellSize(_cellDataIndex) + _space); }
        else { pos.y = firstTrans.anchoredPosition.y + (GetCellSize(_cellDataIndex) + _space); }
        
        lastTrans.anchoredPosition = pos;
        var view = last.Value.GetComponent<ICellView>();
        view.UpdateView(_cellDataList[_cellDataIndex]);
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
        if (_mode == Mode.Horizontal) {
            pos.x = lastTrans.anchoredPosition.x + (lastTrans.sizeDelta.x + _space);
        }
        else {
            pos.y = lastTrans.anchoredPosition.y - (lastTrans.sizeDelta.y + _space);
        }
        
        firstTrans.anchoredPosition = pos;

        var view = first.Value.GetComponent<ICellView>();
        view.UpdateView(_cellDataList[_cellDataIndex + _cellNum]);
    }
}
