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
            List<Dictionary<string, object>> data = CSVReader.Read("Necklace");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Necklace == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Mana = UnityEngine.Random.Range((int)data[0]["최소"], (int)data[0]["마나"]);

                    // 설명
                    createitem.Name = (string)data[0]["이름"];
                    createitem.Description = $"Mana : {createitem.mEffect.Mana.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Mana = UnityEngine.Random.Range((int)data[1]["최소"], (int)data[1]["마나"]);

                    // 설명
                    createitem.Name = (string)data[1]["이름"];
                    createitem.Description = $"Mana : {createitem.mEffect.Mana.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Mana = UnityEngine.Random.Range((int)data[2]["최소"], (int)data[2]["마나"]);

                    // 설명
                    createitem.Name = (string)data[2]["이름"];
                    createitem.Description = $"Mana : {createitem.mEffect.Mana.ToString("F2")}";
                }
            }
            return createitem;
        }
    }
}
