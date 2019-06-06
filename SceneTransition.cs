using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneTransition : SingletonMonobehaviour<SceneTransition>
{
    enum TransitionState
    {
        Wait,
        In,
        Out,
    }
    TransitionState _state = TransitionState.Wait;

    [SerializeField]
    Image _image = null;

    string _sceneName;
    float _fadeOutTimer = 0.0f;
    float _fadeInTimer = 0.0f;
    float _fadeOutTime;
    float _fadeInTime;
    LoadSceneMode _mode = LoadSceneMode.Single;
    public void Request(string sceneName, Color fadeColor, float fadeOutTime = 1.0f, float fadeInTime = 1.0f, LoadSceneMode mode = LoadSceneMode.Single)
    {
        CustomEventSystem.Instance.Current.enabled = false;
        InputManager.Instance.Peek().EnableInput = false;
        _fadeOutTimer = fadeOutTime;
        _fadeOutTime = fadeOutTime;
        _fadeInTimer = fadeInTime;
        _fadeInTime = fadeInTime;
        _sceneName = sceneName;
        _mode = mode;
        _state = TransitionState.Out;
        fadeColor.a = 0.0f;
        _image.color = fadeColor;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void Update()
    {
        //フェードアウト開始
        //フェードアウト終わり
        //シーンロード
        //フェードイン開始
        //フェードイン終わり

        switch (_state)
        {
            case TransitionState.Wait:
                break;
            case TransitionState.In:
                if(_fadeInTimer > 0.0f)
                {
                    _fadeInTimer -= Time.deltaTime;
                    var color = _image.color;
                    color.a = 1.0f - (_fadeInTime - _fadeInTimer) / _fadeInTime;
                    _image.color = color;

                    if (_fadeInTimer < 0.0f)
                    {
                        _fadeInTimer = 0.0f;
                        color.a = 0.0f;
                        _state = TransitionState.Wait;
                        CustomEventSystem.Instance.Current.enabled = true;
                        InputManager.Instance.Peek().EnableInput = true;
                    }
                    _image.color = color;
                }
                break;
            case TransitionState.Out:
                if (_fadeOutTimer > 0.0f)
                {
                    _fadeOutTimer -= Time.deltaTime;
                    var color = _image.color;
                    color.a = (_fadeOutTime - _fadeOutTimer) / _fadeOutTime;
                    if (_fadeOutTimer <= 0.0f)
                    {
                        color.a = 1.0f;
                        _fadeOutTimer = 0.0f;
                        StartCoroutine(LoadScene());
                    }
                    _image.color = color;
                }
                break;
            default:
                break;
        }
    }

    IEnumerator LoadScene()
    {
        var req = SceneManager.LoadSceneAsync(_sceneName, _mode);
        yield return req;
        _state = TransitionState.In;
    }
}
