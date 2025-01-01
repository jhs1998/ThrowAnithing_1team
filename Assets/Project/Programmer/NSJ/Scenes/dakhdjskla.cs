using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dakhdjskla : MonoBehaviour
{
    private void Update()
    {
        float x= Input.GetAxisRaw(InputKey.Negative);
        if ( x != 0)
        {
            Debug.Log(x);
        }
    }
}
