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
            List<Dictionary<string, object>> data = CSVReader.Read("Ring");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Ring == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Damage = UnityEngine.Random.Range((int)data[0]["최소"], (int)data[0]["공격력"]);

                    // 설명
                    createitem.Name = (string)data[0]["이름"];
                    createitem.Description = $"Damage : {createitem.mEffect.Damage.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Damage = UnityEngine.Random.Range((int)data[1]["최소"], (int)data[1]["공격력"]);

                    // 설명
                    createitem.Name = (string)data[1]["이름"];
                    createitem.Description = $"Damage : {createitem.mEffect.Damage.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Damage = UnityEngine.Random.Range((int)data[2]["최소"], (int)data[2]["공격력"]);

                    // 설명
                    createitem.Name = (string)data[2]["이름"];
                    createitem.Description = $"Damage : {createitem.mEffect.Damage.ToString("F2")}";
                }
            }
            return createitem;
        }
    }
}
