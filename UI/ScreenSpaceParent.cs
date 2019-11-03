using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenSpaceParent : MonoBehaviour
{
    [SerializeField]
    Transform target;
    Camera camera;
    Transform parent;
    CanvasScaler canvasScaler;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        parent = transform.parent;

        SetScreenSpacePoint();
    }

    // Update is called once per frame
    void Update()
    {
        SetScreenSpacePoint();
    }

    void SetScreenSpacePoint()
    {
        Vector3 p = camera.WorldToScreenPoint(parent.localPosition);
        p.x -= Screen.width / 2.0f;
        p.y -= Screen.height / 2.0f;
        p.z = 0.0f;
        target.localPosition = p;
    }
}
