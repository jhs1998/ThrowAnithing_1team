using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Pants_", menuName = "Add Item/Equipment_Pants")]
    public class PantsData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);
            Item item = Instantiate(this);
            item.Type = ItemType.Pants;

            if (ItemType.Pants == Type)
            {
                // 주스텟
                creatitem.mEffect.Stemina = UnityEngine.Random.Range(10, 30);
                // 최소 수치 최대수치 넣기
            }
            return creatitem;
        }
    }
}
