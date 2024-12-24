using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Helmet_", menuName = "Add Item/Equipment_Helmet")]
    public class HelmetData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);
            Item item = Instantiate(this);
            item.Type = ItemType.Helmet;

            if (ItemType.Helmet == Type)
            {
                creatitem.mEffect.HP = UnityEngine.Random.Range(10, 20);
                // 최소 수치 최대수치 넣기
            }
            return creatitem;
        }

    }
}
