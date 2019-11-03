using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TriggerEventBase : MonoBehaviour
{
    public UnityAction<Collider> enterAction;
    public UnityAction<Collider> stayAction;
    public UnityAction<Collider> exitAction;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        enterAction?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
        stayAction?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Leave");
        exitAction?.Invoke(other);
    }
}
