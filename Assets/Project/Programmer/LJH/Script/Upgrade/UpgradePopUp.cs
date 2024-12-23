using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePopUp : MonoBehaviour
{
    [SerializeField] GameObject upgrade;
    [SerializeField] GameObject player;
    private void Update()
    {
        if(gameObject.activeSelf)
        {
            if(Input.GetButtonDown("Interaction"))
                upgrade.SetActive(true);

        }
    }

}
