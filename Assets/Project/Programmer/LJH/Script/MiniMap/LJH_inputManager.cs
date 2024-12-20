using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_inputManager : MonoBehaviour
{
    float moveSpeed = 3f;
    Vector3 preFor = Vector3.forward;
    private void Update()
    {
        //Move();
        Rotation();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(x, 0, z) * moveSpeed * Time.deltaTime, Space.Self);
    }

    void Rotation()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if(x != 0 || z !=0)
        {
            preFor = new Vector3(x, 0, z);
        }

        transform.forward = new Vector3(x, 0, z);

        if (x == 0 && z == 0)
            transform.forward = preFor;
    }
}
