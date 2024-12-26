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
                // 장비 등급을 숫자로 변환
                // Normal : 0
                // Magic : 1
                // Rare : 2
                int rate = (int)Rate;

                // 장비 등급 숫자로 주스텟과 설명 추가
                // 주스텟
                createitem.mEffect.Defense = UnityEngine.Random.Range((int)data[rate]["최소"], (int)data[rate]["방어력"]);
                // 설명
                createitem.Name = (string)data[rate]["이름"];
                createitem.Description = $"Defense : {createitem.mEffect.Defense.ToString()}";

                // 부스텟을 모두 0으로 변경
                createitem.mEffect.HP = 0;
                createitem.mEffect.Critical = 0;
                createitem.mEffect.AttackSpeed = 0;
                createitem.mEffect.Stemina = 0;
                createitem.mEffect.EquipRate = 0;
                createitem.mEffect.Damage = 0;
                createitem.mEffect.Speed = 0;
                createitem.mEffect.Mana = 0;

                // 장비 등급 숫자만큼 랜덤한 부스텟 추가
                List<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int i = 0; i < rate; i++)  // Noraml : 0회 반복, Magic : 1회 반복, Rare : 2회 반복
                {
                    int select = list[Random.Range(0, list.Count)];
                    list.Remove(select);

                    switch (select)
                    {
                        case 0:
                            createitem.mEffect.HP = (int)data[rate]["체력"];
                            createitem.Description += $"\nHP : {createitem.mEffect.HP.ToString()}";
                            break;
                        case 1:
                            createitem.mEffect.Critical = (int)data[rate]["치확"];
                            createitem.Description += $"\nCritical : {createitem.mEffect.Critical.ToString()}";
                            break;
                        case 2:
                            createitem.mEffect.AttackSpeed = (float)data[rate]["공속"];
                            createitem.Description += $"\nAttackSpeed : {createitem.mEffect.AttackSpeed.ToString("F2")}";
                            break;
                        case 3:
                            createitem.mEffect.Stemina = (int)data[rate]["스테미나"];
                            createitem.Description += $"\nStemina : {createitem.mEffect.Stemina.ToString()}";
                            break;
                        case 4:
                            createitem.mEffect.EquipRate = (float)data[rate]["장비획득률 증가"];
                            createitem.Description += $"\nEquipRate : {createitem.mEffect.EquipRate.ToString("F2")}";
                            break;
                        case 5:
                            createitem.mEffect.Damage = (int)data[rate]["공격력"];
                            createitem.Description += $"\nDamage : {createitem.mEffect.Damage.ToString()}";
                            break;
                        case 6:
                            createitem.mEffect.Speed = (float)data[rate]["이속"];
                            createitem.Description += $"\nSpeed : {createitem.mEffect.Speed.ToString("F2")}";
                            break;
                        case 7:
                            createitem.mEffect.Mana = (int)data[rate]["마나"];
                            createitem.Description += $"\nMana : {createitem.mEffect.Mana.ToString()}";
                            break;
                    }

                }
            }
            return createitem;
        }
    }
}
