using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePopUp : MonoBehaviour
{
    [SerializeField] GameObject _object;
    //[SerializeField] GameObject player;
    private void Update()
    {
        if(gameObject.activeSelf&& _object.activeSelf == false && Time.deltaTime != 0)
        {
            if(InputKey.GetButtonDown(InputKey.PrevInteraction))
                _object.SetActive(true);
        }
    }

}
