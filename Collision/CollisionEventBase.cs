using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CollisionEventBase : MonoBehaviour
{
    public UnityAction enterAction;
    public UnityAction stayAction;
    public UnityAction exitAction;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CollisionEnter");
        enterAction?.Invoke();
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("CollisionStay");
        stayAction?.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("CollisionExit");
        exitAction?.Invoke();
    }
}
