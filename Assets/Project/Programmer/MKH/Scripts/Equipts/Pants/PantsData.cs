using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Pants_", menuName = "Add Item/Equipment_Pants")]
    public class PantsData : Item_Equipment
    {
        public override Item Create()
        {
            List<Dictionary<string, object>> data = CSVReader.Read("Pants");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Pants == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Stemina = UnityEngine.Random.Range((int)data[0]["최소"], (int)data[0]["스테미나"]);

                    // 설명
                    createitem.Name = (string)data[0]["이름"];
                    createitem.Description = $"Stemina : {createitem.mEffect.Stemina.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Stemina = UnityEngine.Random.Range((int)data[1]["최소"], (int)data[1]["스테미나"]);

                    // 설명
                    createitem.Name = (string)data[1]["이름"];
                    createitem.Description = $"Stemina : {createitem.mEffect.Stemina.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // 주스텟
                    createitem.mEffect.Stemina = UnityEngine.Random.Range((int)data[2]["최소"], (int)data[2]["스테미나"]);

                    // 설명
                    createitem.Name = (string)data[2]["이름"];
                    createitem.Description = $"Stemina : {createitem.mEffect.Stemina.ToString("F2")}";
                }
            }
            return createitem;
        }
    }
}
