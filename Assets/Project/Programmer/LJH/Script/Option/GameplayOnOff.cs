using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayOnOff : MonoBehaviour
{
    [SerializeField] Main_Option option;
    private void OnDisable()
    {
        //option.optionCheck--;
    }
}
