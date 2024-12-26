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
            List<Dictionary<string, object>> data = CSVReader.Read("Shoes");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Shoes == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Speed = UnityEngine.Random.Range((float)data[0]["최소"], (float)data[0]["이속"]);

                    // 설명
                    createitem.Name = (string)data[0]["이름"];
                    createitem.Description = $"Speed : {createitem.mEffect.Speed.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Speed = UnityEngine.Random.Range((float)data[1]["최소"], (float)data[1]["이속"]);

                    // 설명
                    createitem.Name = (string)data[1]["이름"];
                    createitem.Description = $"Speed : {createitem.mEffect.Speed.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Speed = UnityEngine.Random.Range((float)data[2]["최소"], (float)data[2]["이속"]);

                    // 설명
                    createitem.Name = (string)data[2]["이름"];
                    createitem.Description = $"Speed : {createitem.mEffect.Speed.ToString("F2")}";
                }
            }
            return createitem;
        }
    }
}
