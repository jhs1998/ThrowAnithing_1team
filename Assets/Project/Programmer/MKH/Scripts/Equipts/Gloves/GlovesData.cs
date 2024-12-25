using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Gloves_", menuName = "Add Item/Equipment_Gloves")]
    public class GlovesData : Item_Equipment
    {
        public override Item Create()
        {
            List<Dictionary<string, object>> data = CSVReader.Read("Gloves");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Gloves == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // 주스텟
                    createitem.mEffect.AttackSpeed = UnityEngine.Random.Range((float)data[0]["최소"], (float)data[0]["공속"]);

                    // 설명
                    createitem.Name = (string)data[0]["이름"];
                    createitem.Description = $"AttackSpeed : {createitem.mEffect.AttackSpeed.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    createitem.mEffect.AttackSpeed = UnityEngine.Random.Range((float)data[1]["최소"], (float)data[1]["공속"]);

                    // 설명
                    createitem.Name = (string)data[1]["이름"];
                    createitem.Description = $"AttackSpeed : {createitem.mEffect.AttackSpeed.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    createitem.mEffect.AttackSpeed = UnityEngine.Random.Range((float)data[2]["최소"], (float)data[2]["공속"]);

                    // 설명
                    createitem.Name = (string)data[2]["이름"];
                    createitem.Description = $"AttackSpeed : {createitem.mEffect.AttackSpeed.ToString("F2")}";
                }

            }
            return createitem;
        }
    }
}
