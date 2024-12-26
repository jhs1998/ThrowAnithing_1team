using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTest : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Tag.Player)
            Loading.LoadScene("GameSceneSta1");
    }

}
