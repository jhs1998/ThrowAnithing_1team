using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSJ_TesterPanel
{
    public class TesterMonster : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F4))
            {
                IHit hit = GetComponent<IHit>();
                hit.TakeDamage(10000000, true);
            }   
        }
    }
}