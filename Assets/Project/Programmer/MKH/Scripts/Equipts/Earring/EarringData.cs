using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Earring_", menuName = "Add Item/Equipment_Earring")]
    public class EarringData : Item_Equipment
    {
        public override Item Create()
        {
            Item_Equipment creatitem = Instantiate(this);

            if (ItemType.Earring == Type)
            {
                // 주스텟
                creatitem.mEffect.EquipRate = UnityEngine.Random.Range(0.01f, 0.1f);
                // 최소 수치 최대수치 넣기
                creatitem.Description = "EquipRate : " + creatitem.mEffect.EquipRate.ToString("F2");
            }
            return creatitem;
        }
    }
}
