using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Earring_", menuName = "Add Item/Equipment_Earring")]
    public class EarringData : Item_Equipment
    {
        public override Item Create()
        {
            List<Dictionary<string, object>> data = CSVReader.Read("Earring");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Earring == Type)
            {
                if (RateType.Nomal == Rate)
                {
                    // ¡÷Ω∫≈›
                    createitem.mEffect.EquipRate = UnityEngine.Random.Range((float)data[0]["√÷º“"], (float)data[0]["¿Â∫Ò»πµÊ∑¸ ¡ı∞°"]);

                    // º≥∏Ì
                    createitem.Name = (string)data[0]["¿Ã∏ß"];
                    createitem.Description = $"EquipRate : {createitem.mEffect.EquipRate.ToString("F2")}";
                }
                else if (RateType.Magic == Rate)
                {
                    // ¡÷Ω∫≈›
                    createitem.mEffect.EquipRate = UnityEngine.Random.Range((float)data[1]["√÷º“"], (float)data[1]["¿Â∫Ò»πµÊ∑¸ ¡ı∞°"]);

                    // º≥∏Ì
                    createitem.Name = (string)data[1]["¿Ã∏ß"];
                    createitem.Description = $"EquipRate : {createitem.mEffect.EquipRate.ToString("F2")}";
                }
                else if (RateType.Rare == Rate)
                {
                    // ¡÷Ω∫≈›
                    createitem.mEffect.EquipRate = UnityEngine.Random.Range((float)data[2]["√÷º“"], (float)data[2]["¿Â∫Ò»πµÊ∑¸ ¡ı∞°"]);

                    // º≥∏Ì
                    createitem.Name = (string)data[2]["¿Ã∏ß"];
                    createitem.Description = $"EqipRate : {createitem.mEffect.EquipRate.ToString("F2")}";
                }
            }

            return createitem;

        }
    }
}
