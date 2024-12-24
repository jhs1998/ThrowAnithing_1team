using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayOnOff : MonoBehaviour
{
    [SerializeField] Main_Option option;
    private void OnDisable()
    {
        option.depth1_cur = 0;
    }
}
