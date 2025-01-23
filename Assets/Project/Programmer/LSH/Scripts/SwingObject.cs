using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingObject : MonoBehaviour
{
    [Range(1f, 5f)] [SerializeField] float swingSpeed;  // ������ ��鸮�� �ӵ�
    [SerializeField] float swingAngle = 30.0f; // ������ �ִ� ��鸮�� ����

    [SerializeField] GameObject triggerArea;
    [Range(0.5f, 1.0f)] [SerializeField] float triggerSize;

    void Update()
    {
        // swing
        float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;

        Debug.Log(angle);

        // ���� rotation�� ��ȭ
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                              transform.rotation.eulerAngles.y, 
                                              angle);




        triggerArea.GetComponent<SphereCollider>().radius = triggerSize;
    }

    
}
