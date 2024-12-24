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
        if(other.gameObject.tag == Tag.BlueChip)
        {
            choice.SetActive(true);
           
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == Tag.BlueChip)
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
        if(other.gameObject.tag == Tag.BlueChip)
        {
            choice.SetActive(false);
        }
    }

    private void Open()
    {
       
    }
}
