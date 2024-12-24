using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Shirts_", menuName = "Add Item/Equipment_Shirts")]
    public class ShirtsData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);
            Item item = Instantiate(this);
            item.Type = ItemType.Shirts;

            if (ItemType.Shirts == Type)
            {
                // 주스텟
                creatitem.mEffect.Defense = UnityEngine.Random.Range(1, 1);
                // 최소 수치 최대수치 넣기
            }
            return creatitem;
        }
    }
}
