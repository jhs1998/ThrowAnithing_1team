using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueChipChoice : MonoBehaviour
{
    [SerializeField] GameObject choice;

    private void Start()
    {
        choice.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Trash"))
        {
            choice.SetActive(true);
           
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Trash"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                choice.SetActive(false);
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Trash"))
        {
            choice.SetActive(false);
        }
    }

    private void Open()
    {
       
    }
}
