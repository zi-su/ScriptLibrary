using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HitAreaBase : MonoBehaviour
{
    public UnityAction enterAction;
    public UnityAction stayAction;
    public UnityAction exitAction;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        enterAction?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
        stayAction?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Leave");
        exitAction?.Invoke();
    }
}
