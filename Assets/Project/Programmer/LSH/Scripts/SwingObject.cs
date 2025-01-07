using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingObject : MonoBehaviour
{
    [SerializeField] float swingSpeed = 2.0f;  // 판자의 흔들리는 속도
    [SerializeField] float swingAngle = 30.0f; // 판자의 최대 흔들리는 각도

    void Update()
    {
        // swing
        float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;

        Debug.Log(angle);

        // 판자 rotation값 변화
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                              transform.rotation.eulerAngles.y, 
                                              angle);


    }
}
