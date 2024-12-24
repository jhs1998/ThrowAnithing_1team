using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Necklace_", menuName = "Add Item/Equipment_Necklace")]
    public class NecklaceData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);

            if (ItemType.Necklace == Type)
            {
                // 주스텟
                creatitem.mEffect.Mana = UnityEngine.Random.Range(10, 20);
                // 최소 수치 최대수치 넣기
            }
            return creatitem;
        }
    }
}
