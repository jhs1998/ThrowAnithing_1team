using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePopUp : MonoBehaviour
{
    [SerializeField] GameObject _object;
    //[SerializeField] GameObject player;
    private void Update()
    {
        if(gameObject.activeSelf && Time.deltaTime != 0)
        {
            if(InputKey.GetButtonDown(InputKey.Interaction))
                _object.SetActive(true);
        }
    }

}
