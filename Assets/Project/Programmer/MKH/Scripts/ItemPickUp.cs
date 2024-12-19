using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    public class ItemPickUp : MonoBehaviour
    {
        [Header("해당 오브젝트에 할당되는 아이템")]
        [SerializeField] private Item mItem;
        public Item Item { get { return mItem; } }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if(other.CompareTag("Player"))
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }
}
