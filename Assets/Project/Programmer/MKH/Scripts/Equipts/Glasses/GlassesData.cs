using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Glasses_", menuName = "Add Item/Equipment_Glasses")]
    public class GlassesData : Item_Equipment
    {
        public override Item Create()
        {
            List<Dictionary<string, object>> data = CSVReader.Read("Glasses");
            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Glasses == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Critical = UnityEngine.Random.Range((int)data[0]["최소"], (int)data[0]["치확"]);

                    // 설명
                    createitem.Name = (string)data[0]["이름"];
                    createitem.Description = $"Critical : {createitem.mEffect.Critical.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Critical = UnityEngine.Random.Range((int)data[1]["최소"], (int)data[1]["치확"]);

                    // 설명
                    createitem.Name = (string)data[1]["이름"];
                    createitem.Description = $"Critical : {createitem.mEffect.Critical.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Critical = UnityEngine.Random.Range((int)data[2]["최소"], (int)data[2]["치확"]);

                    // 설명
                    createitem.Name = (string)data[2]["이름"];
                    createitem.Description = $"Critical : {createitem.mEffect.Critical.ToString("F2")}";
                }
            }
            return createitem;
        }

    }
}
