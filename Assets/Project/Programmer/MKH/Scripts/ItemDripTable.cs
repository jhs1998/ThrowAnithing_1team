using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu]
    public class ItemDripTable : ScriptableObject
    {
        [System.Serializable]
        public class Items
        {
            public Item_Equipment equipment;
            public int weight;
        }

        public List<Items> items = new List<Items>();

        protected Item_Equipment PickItem()
        {
            int sum = 0;
            foreach (var item in items)
            {
                sum += item.weight;
            }

            var rnd = Random.Range(0, sum);

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (item.weight > rnd) return items[i].equipment;
                else rnd -= item.weight;
            }
            return null;
        }

        public void ItemDrop(Vector3 pos)
        {
            var item = PickItem();
            if (item == null)
                return;

            Instantiate(item.Prefab, pos, Quaternion.identity);
        }
    }
}
