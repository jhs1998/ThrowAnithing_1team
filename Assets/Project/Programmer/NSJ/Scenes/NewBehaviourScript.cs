using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Update()
    {
        Debug.Log(InputKey.GetAxis(InputKey.Dash));
    }
}
