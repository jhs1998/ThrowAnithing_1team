using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Shoes_", menuName = "Add Item/Equipment_Shoes")]
    public class ShoesData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);

            if (ItemType.Shoes == Type)
            {
                // 주스텟
                creatitem.mEffect.Speed = UnityEngine.Random.Range(0.05f, 0.1f);
                // 최소 수치 최대수치 넣기
            }
            return creatitem;
        }
    }
}
