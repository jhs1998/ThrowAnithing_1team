using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour
{
    [SerializeField] GameObject upPopup;
    [SerializeField] GameObject _upgrade;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(Tag.Player))
        {
            upPopup.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Player))
        {
            upPopup.SetActive(false);     
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown(InputKey.Negative))
        {
            if(_upgrade.activeSelf == true)
            {
                _upgrade.SetActive(false);
            }
        }
    }
}
