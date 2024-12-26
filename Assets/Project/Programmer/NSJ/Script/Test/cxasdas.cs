using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cxasdas : MonoBehaviour
{
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Debug.Log(camera.cullingMask);
       
    }
}
