using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// データリストをもとにしてスクロール内の要素(セル）を
/// 使いまわして高速なスクロールを行うためのコントローラクラス
/// Cellプレハブは
/// Verticalの場合、Pivot(0.5f,1.0f)、Anchors(min(0.5,1),max(0.5,1))
/// Horizontalの場合、Pivot(0.0f, 0.5f), Anchors(min(0,0.5f),max(0.0f, 0.5f))
/// </summary>
public class ScrollController : MonoBehaviour
{
    enum Mode
    {
        Horizontal,
        Vertical,
    }
    [SerializeField]
    ScrollRect _scrollRect = null;
    [SerializeField]
    Mode _mode = Mode.Horizontal;
    [SerializeField]
    float _spaceX = 0.0f;
    [SerializeField]
    float _spaceY = 0.0f;
    [SerializeField]
    float _marginTop = 0.0f;
    [SerializeField]
    float _marginBottom = 0.0f;

    [SerializeField]
    GameObject _cellPrefab = null;
    [SerializeField]
    RectTransform _scrollRectTransform = null;
    [SerializeField]
    RectTransform _contentRectTransform = null;

    List<ICellData> _cellDataList = new List<ICellData>();
    LinkedList<GameObject> _cellLinkList = new LinkedList<GameObject>();
    List<float> _cellPositionList = new List<float>();
    int _cellNum = 0;

    int _cellDataIndex = 0;

    float _diffMove;

    float GetCellSize(int dataIndex)
    {
        return _mode == Mode.Horizontal ? _cellDataList[dataIndex].GetCellSize().x : _cellDataList[dataIndex].GetCellSize().y;
    }

    float GetContentAnchoredPos()
    {
        return _mode == Mode.Horizontal ? -_contentRectTransform.anchoredPosition.x : _contentRectTransform.anchoredPosition.y;
    }

    float GetContentSize()
    {
        return _mode == Mode.Horizontal ? _contentRectTransform.sizeDelta.x : _contentRectTransform.sizeDelta.y;
    }
    
    float GetSpace()
    {
        return _mode == Mode.Horizontal ? _spaceX : _spaceY;
    }
    // Start is called before the first frame update
    void Start()
    {
        //テストコード
        TestData();
    }

    void TestData()
    {
        List<ICellData> list = new List<ICellData>()
        {
            new CellDataBase(100.0f, 100.0f),
            new CellDataBase(100.0f, 150.0f),
            new CellDataBase(100.0f, 50.0f),
            new CellDataBase(100.0f, 200.0f),
            new CellDataBase(100.0f, 100.0f),
            new CellDataBase(100.0f, 150.0f),
            new CellDataBase(100.0f, 100.0f),
            new CellDataBase(100.0f, 100.0f),
            new CellDataBase(100.0f, 250.0f),
        };

        Initialize(list);
    }
    public void Initialize(List<ICellData> dataList)
    {
        _cellDataList.AddRange(dataList);
        CalcContentSize();
        _cellNum = CalcCellNum();
        CalcCellPosFromDataList();
        for (int i = 0; i < _cellNum; i++)
        {
            InstantiateCell(i);
        }
        AlignCell();
    }

    /// <summary>
    /// 新しいデータリストでセルを更新する
    /// </summary>
    /// <param name="dataList"></param>
    public void Refresh(List<ICellData> dataList)
    {
        _cellDataList.Clear();
        _cellDataList.AddRange(dataList);
        CalcContentSize();
        _cellNum = CalcCellNum();
        CalcCellPosFromDataList();
        ResetNormalizePosition();
        RefreshCell();
        AlignCell();

    }

    void ResetNormalizePosition()
    {
        var pos = _contentRectTransform.anchoredPosition;
        if (_mode == Mode.Horizontal) {
            pos.x = 0.0f;
            _scrollRect.horizontalNormalizedPosition = 0.0f;
            _diffMove = 0.0f;
            _cellDataIndex = 0;
        }
        else {
            pos.y = 0.0f;
            _scrollRect.verticalNormalizedPosition = 1.0f;
            _diffMove = 0.0f;
            _cellDataIndex = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(_cellDataList.Count == 0) { return; }

        ScrollDown();
        ScrollUp();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            List<ICellData> d = new List<ICellData>()
            {
                new CellDataBase(100.0f, 100.0f),
            new CellDataBase(200.0f, 200.0f),
            new CellDataBase(100.0f, 100.0f),
            new CellDataBase(100.0f, 200.0f)
            };
            Refresh(d);
        }
    }

    void ScrollDown()
    {
        while (GetContentAnchoredPos() + _diffMove > _marginTop + GetCellSize(_cellDataIndex))
        {
            Debug.Log("ScrollDown");
            if (_cellDataIndex + _cellNum >= _cellDataList.Count) { return; }
            _diffMove -= (GetCellSize(_cellDataIndex)) + GetSpace();

            
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
            Debug.Log("ScrollUp");
            if (GetContentAnchoredPos() < 0.0f) { return; }
            
            _cellDataIndex--;
            _diffMove += (GetCellSize(_cellDataIndex)) + GetSpace();

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
        size += GetSpace() * (_cellDataList.Count - 1);
        size += _marginBottom;
        Vector2 sizeDelta = _contentRectTransform.sizeDelta;
        if (_mode == Mode.Horizontal) { sizeDelta.x = size; }
        else { sizeDelta.y = size; }
        _contentRectTransform.sizeDelta = sizeDelta;
    }

    /// <summary>
    /// データリストの最小セルサイズから表示するセルの数を計算
    /// </summary>
    /// <returns></returns>
    int CalcCellNum()
    {
        int num = 0;
        float scrollSize = _mode == Mode.Horizontal ? _scrollRectTransform.sizeDelta.x : _scrollRectTransform.sizeDelta.y;
        float minCellSize = int.MaxValue;
        for (int i = 0; i < _cellDataList.Count; i++)
        {
            if (GetCellSize(i) < minCellSize)
            {
                minCellSize = GetCellSize(i);
            }
        }
        scrollSize -= _marginTop;
        for(int i = 0; i < _cellDataList.Count; i++)
        {
            scrollSize -= minCellSize;
            scrollSize -= i == 0 ? 0.0f : GetSpace();
            if(scrollSize < 0)
            {
                num++;
                break;
            }
            num++;
        }
        num++;
        if(num > _cellDataList.Count)
        {
            num = _cellDataList.Count;
        }
        return num;
    }

    /// <summary>
    /// セルの生成処理
    /// </summary>
    void InstantiateCell(int i)
    {
        var cell = Instantiate(_cellPrefab, _contentRectTransform);
        var view = cell.GetComponent<ICellView>();
        view.UpdateView(_cellDataList[i]);
        _cellLinkList.AddLast(cell);
    }

    /// <summary>
    /// データリストから各セル座標を計算してリストに保存
    /// </summary>
    void CalcCellPosFromDataList()
    {
        float p = 0.0f;
        _cellPositionList.Clear();
        for(int i = 0; i < _cellDataList.Count; i++)
        {
            p += i == 0 ? _marginTop : GetCellSize(i - 1) + GetSpace();
            _cellPositionList.Add(p);
        }
    }

    /// <summary>
    /// 座標リストからセルの座標を取得して設定
    /// </summary>
    void AlignCell()
    {
        for(int i = 0; i < _cellNum; i++)
        {
            var cell = _contentRectTransform.GetChild(i) as RectTransform;
            Vector3 pos = cell.anchoredPosition;
            if (_mode == Mode.Horizontal) { pos.x = _cellPositionList[i]; }
            else { pos.y = -_cellPositionList[i]; }
            cell.anchoredPosition = pos;
        }
        
    }
    void RefreshCell()
    {
        //多ければ生成、少なければ破棄
        if (_cellNum > _contentRectTransform.childCount)
        {
            var count = _cellNum - _contentRectTransform.childCount;
            for (int i = 0; i < count; i++)
            {
                Instantiate(_cellPrefab, _contentRectTransform);
            }
        }
        else if(_cellNum < _contentRectTransform.childCount)
        {
            int count = _contentRectTransform.childCount;
            for (int i = _cellNum; i < count; i++)
            {
                Destroy(_contentRectTransform.GetChild(i).gameObject);
            }
        }

        //リンクリスト初期化
        _cellLinkList.Clear();
        for(int i = 0; i < _cellNum; i++)
        {
            _cellLinkList.AddLast(_contentRectTransform.GetChild(i).gameObject);
        }

        //表示内容を更新
        for(int i = 0; i < _cellNum; i++)
        {
            var view = _contentRectTransform.GetChild(i).GetComponent<ICellView>();
            view.UpdateView(_cellDataList[i]);
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
        if (_mode == Mode.Horizontal) { pos.x = firstTrans.anchoredPosition.x - (GetCellSize(_cellDataIndex) + GetSpace()); }
        else { pos.y = firstTrans.anchoredPosition.y + (GetCellSize(_cellDataIndex) + GetSpace()); }
        
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
            pos.x = lastTrans.anchoredPosition.x + (lastTrans.sizeDelta.x + GetSpace());
        }
        else {
            pos.y = lastTrans.anchoredPosition.y - (lastTrans.sizeDelta.y + GetSpace());
        }
        
        firstTrans.anchoredPosition = pos;

        var view = first.Value.GetComponent<ICellView>();
        view.UpdateView(_cellDataList[_cellDataIndex + _cellNum]);
    }
}
