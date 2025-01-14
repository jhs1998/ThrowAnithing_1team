using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (InputKey.GetButtonUp(InputKey.Throw))
        {
            Debug.Log("´©¸§");
        }
    }
}
