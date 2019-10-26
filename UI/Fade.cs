using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : SingletonMonobehaviour<Fade>
{
    Canvas _canvas;
    GameObject _fade;
    Image _fadeImage;
    float _time;
    float _elaps;
    Color _toColor;
    Color _fromColor;
    System.Action _action;

    /// <summary>
    /// Canvas描画順序
    /// </summary>
    readonly int sortingOrder = 999;
    public enum Mode
    {
        Idle,
        In,
        Out,
    }
    Mode _mode = Mode.Idle;

    protected override void Awake()
    {
        base.Awake();
        CreateCanvas();

        CreateImage();

        _canvas.enabled = false;
    }
    void CreateCanvas()
    {
        //Canvasコンポーネント
        _canvas = gameObject.AddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        _canvas.sortingOrder = sortingOrder;
        //CanvasScaler
        var scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(Screen.width, Screen.height);

        //GraphicsRayCaster
        gameObject.AddComponent<GraphicRaycaster>();
    }
    void CreateImage()
    {
        _fade = new GameObject("fadeImage", typeof(Image));
        _fade.transform.SetParent(transform);
        var rect = _fade.transform as RectTransform;
        Vector2 min = new Vector2(0.0f, 0.0f);
        Vector2 max = new Vector2(1.0f, 1.0f);
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.anchoredPosition = new Vector2(0.0f, 0.0f);
        rect.sizeDelta = new Vector2(0.0f, 0.0f);
        _fadeImage = _fade.GetComponent<Image>();
        _fadeImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    /// <summary>
    /// 現在色からtoColorへフェード
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="toColor"></param>
    /// <param name="time">秒指定</param>
    /// <param name="action">フェード終了時のコールバック</param>
    public void Play(Mode mode, Color toColor, float time = 1.0f, System.Action action = null)
    {
        _mode = mode;
        _canvas.enabled = true;
        _time = time;
        _elaps = time;
        _toColor = toColor;
        _fromColor = _fadeImage.color;
        _action = action;
    }

    /// <summary>
    /// fromColorからtoColorへフェード
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="toColor"></param>
    /// <param name="fromColor"></param>
    /// <param name="time">秒指定</param>
    /// <param name="action">フェード終了時のコールバック</param>
    public void Play(Mode mode, Color toColor, Color fromColor, float time = 1.0f, System.Action action = null)
    {
        _mode = mode;
        _canvas.enabled = true;
        _time = time;
        _elaps = time;
        _toColor = toColor;
        _fadeImage.color = fromColor;
        _fromColor = fromColor;
        _action = action;
    }

    public bool IsFading()
    {
        return _mode == Mode.In || _mode == Mode.Out;
    }
    private void Update()
    {
        switch (_mode)
        {
            case Mode.Idle:
                break;
            case Mode.In:
                _fadeImage.color = Color.Lerp(_toColor, _fromColor, _elaps / _time);
                _elaps -= Time.deltaTime;
                if(_elaps < 0.0f)
                {
                    _fadeImage.color = _toColor;
                    _mode = Mode.Idle;
                    _canvas.enabled = false;
                    _action?.Invoke();
                }
                break;
            case Mode.Out:
                _fadeImage.color = Color.Lerp(_toColor, _fromColor, _elaps / _time);
                _elaps -= Time.deltaTime;
                if (_elaps < 0.0f)
                {
                    _fadeImage.color = _toColor;
                    _mode = Mode.Idle;
                    _action?.Invoke();
                }
                break;
            default:
                break;
        }
    }
}
