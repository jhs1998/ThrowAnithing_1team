using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEditor.UIElements;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Helmet_", menuName = "Add Item/Equipment_Helmet")]
    public class HelmetData : Item_Equipment
    {
        public override Item Create()
        {
            List<Dictionary<string, object>> data = CSVReader.Read("Helmet");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Helmet == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // 주스텟
                    createitem.mEffect.HP = UnityEngine.Random.Range((int)data[0]["최소"], (int)data[0]["체력"]);

                    // 설명
                    createitem.Name = (string)data[0]["이름"];
                    createitem.Description = $"HP : {createitem.mEffect.HP.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    createitem.mEffect.HP = UnityEngine.Random.Range((int)data[1]["최소"], (int)data[1]["체력"]);

                    // 설명
                    createitem.Name = (string)data[1]["이름"];
                    createitem.Description = $"HP : {createitem.mEffect.HP.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    createitem.mEffect.HP = UnityEngine.Random.Range((int)data[2]["최소"], (int)data[2]["체력"]);

                    // 설명
                    createitem.Name = (string)data[2]["이름"];
                    createitem.Description = $"HP : {createitem.mEffect.HP.ToString("F2")}";
                }
            }
            return createitem;
        }

    }
}
