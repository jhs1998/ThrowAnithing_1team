using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSJ_TesterPanel
{
    public class TesterMonster : MonoBehaviour
    {
        public void Die()
        {
            IHit hit = GetComponent<IHit>();
            hit.TakeDamage(10000000, false, CrowdControlType.Stiff);
        }
    }
}