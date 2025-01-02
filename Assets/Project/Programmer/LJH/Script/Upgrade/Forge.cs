using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour
{
    [SerializeField] GameObject upPopup;
    [SerializeField] GameObject _ui;

    public bool IsUIActive;
    
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
            _ui.SetActive(false);
        }
    }

    private void Update()
    {
        if (InputKey.GetButtonDown(InputKey.Negative))
        {
            if(_ui.activeSelf == true && IsUIActive == true)
            {
                _ui.SetActive(false);
            }
        }
        
        //UI 활성화 됬는지 감지
        if(_ui.activeSelf == true)
        {
            IsUIActive = true;
        }
        else
        {
            IsUIActive = false;
        }    
    }
}
