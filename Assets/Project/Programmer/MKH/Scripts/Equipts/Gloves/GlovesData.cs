using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Gloves_", menuName = "Add Item/Equipment_Gloves")]
    public class GlovesData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);
            Item item = Instantiate(this);
            item.Type = ItemType.Gloves;

            if (ItemType.Gloves == Type)
            {
                // 주스텟
                creatitem.mEffect.AttackSpeed = UnityEngine.Random.Range(0.1f, 0.2f);
                // 최소 수치 최대수치 넣기

            }
            return creatitem;
        }
    }
}
