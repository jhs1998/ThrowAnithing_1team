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
            List<Dictionary<string, object>> data = CSVReader.Read("Shirts");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Shirts == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Defense = UnityEngine.Random.Range((int)data[0]["최소"], (int)data[0]["방어력"]);

                    // 설명
                    createitem.Name = (string)data[0]["이름"];
                    createitem.Description = $"Defence : {createitem.mEffect.Defense.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Defense = UnityEngine.Random.Range((int)data[1]["최소"], (int)data[1]["방어력"]);

                    // 설명
                    createitem.Name = (string)data[1]["이름"];
                    createitem.Description = $"Defence : {createitem.mEffect.Defense.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Defense = UnityEngine.Random.Range((int)data[2]["최소"], (int)data[2]["방어력"]);

                    // 설명
                    createitem.Name = (string)data[2]["이름"];
                    createitem.Description = $"Defence : {createitem.mEffect.Defense.ToString("F2")}";
                }
            }
            return createitem;
        }
    }
}
