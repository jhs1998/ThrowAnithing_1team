using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Ring_", menuName = "Add Item/Equipment_Ring")]
    public class RingData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);

            if (ItemType.Ring == Type)
            {
                // 주스텟
                creatitem.mEffect.Damage = UnityEngine.Random.Range(5, 10);
                // 최소 수치 최대수치 넣기
            }
            return creatitem;
        }
    }
}
