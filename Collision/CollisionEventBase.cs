using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CollisionEventBase : MonoBehaviour
{
    public UnityAction<Collision> enterAction;
    public UnityAction<Collision> stayAction;
    public UnityAction<Collision> exitAction;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CollisionEnter");
        enterAction?.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("CollisionStay");
        stayAction?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("CollisionExit");
        exitAction?.Invoke(collision);
    }
}
