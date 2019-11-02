using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ButtonBase : MonoBehaviour
{
    /// <summary>
    /// ボタンの選択、解除、クリック時のアニメーター
    /// </summary>
    private Animator animator;

    /// <summary>
    /// 各イベントごとのデリゲート
    /// </summary>
    public UnityAction OnEnter { get; set; }
    public UnityAction OnExit { get; set; }
    public UnityAction OnClick { get; set; }
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public virtual void Enter()
    {
       if(animator != null)
        {
            animator.Play("Enter");
        }
        OnEnter?.Invoke();
    }

    public virtual void Exit()
    {
        if (animator != null)
        {
            animator.Play("Exit");
        }
        OnExit?.Invoke();
    }

    public virtual void Click()
    {
        if (animator != null)
        {
            animator.Play("Click");
        }
        OnClick?.Invoke();
    }
}
