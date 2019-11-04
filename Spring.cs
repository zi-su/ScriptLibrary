using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField]
    Transform _targetTranform = null;
    [SerializeField]
    float _k = 1.0f;   //ばね定数
    [SerializeField]
    float _mass = 1.0f;    //質量
    Transform _thisTransform;
    // Start is called before the first frame update
    void Start()
    {
        _thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //外力計算
        float x = (_targetTranform.position - _thisTransform.position).magnitude;
        float f = _k * x;

        //加速度微分。質量1前提
        float accelaration = f / _mass;
        float v = accelaration * Time.deltaTime;

        Vector3 dir = (_targetTranform.position - _thisTransform.position).normalized;
        _thisTransform.Translate(v * dir);
    }
}
