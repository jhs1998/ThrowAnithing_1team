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
            List<Dictionary<string, object>> data = CSVReader.Read("EquipCSV - Ring");

            Item_Equipment createitem = Instantiate(this);

            if (ItemType.Ring == Type)
            {
                // ��� ����� ���ڷ� ��ȯ
                // Normal : 0
                // Magic : 1
                // Rare : 2
                int rate = (int)Rate;

                // ��� ��� ���ڷ� �ֽ��ݰ� ���� �߰�
                // �ֽ���
                createitem.mEffect.Damage = Random.Range((int)data[rate]["�ּ�"], (int)data[rate]["���ݷ�"]);
                // ����
                createitem.Name = (string)data[rate]["�̸�"];
                createitem.Description = $"���ݷ� + {createitem.mEffect.Damage.ToString()}";

                // �ν����� ��� 0���� ����
                createitem.mEffect.HP = 0;
                createitem.mEffect.Defense = 0;
                createitem.mEffect.Critical = 0;
                createitem.mEffect.AttackSpeed = 0;
                createitem.mEffect.Stemina = 0;
                createitem.mEffect.EquipRate = 0;
                createitem.mEffect.Speed = 0;
                createitem.mEffect.Mana = 0;

                // ��� ��� ���ڸ�ŭ ������ �ν��� �߰�
                List<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int i = 0; i < rate; i++)  // Noraml : 0ȸ �ݺ�, Magic : 1ȸ �ݺ�, Rare : 2ȸ �ݺ�
                {
                    int select = list[Random.Range(0, list.Count)];
                    list.Remove(select);

                    switch (select)
                    {
                        case 0:
                            createitem.mEffect.HP = (int)data[rate]["ü��"];
                            createitem.Description += $"\nü�� + {createitem.mEffect.HP.ToString()}";
                            break;
                        case 1:
                            createitem.mEffect.Defense = (int)data[rate]["����"];
                            createitem.Description += $"\n���� + {createitem.mEffect.Defense.ToString()}";
                            break;
                        case 2:
                            createitem.mEffect.Critical = (int)data[rate]["ġȮ"];
                            createitem.Description += $"\nġ��Ÿ Ȯ�� + {createitem.mEffect.Critical.ToString()}%";
                            break;
                        case 3:
                            createitem.mEffect.AttackSpeed = (float)data[rate]["����"];
                            createitem.Description += $"\n���ݼӵ� + {(createitem.mEffect.AttackSpeed * 100f).ToString()}%";
                            break;
                        case 4:
                            createitem.mEffect.Stemina = (int)data[rate]["���׹̳�"];
                            createitem.Description += $"\n���׹̳� + {createitem.mEffect.Stemina.ToString()}";
                            break;
                        case 5:
                            createitem.mEffect.EquipRate = (float)data[rate]["���ȹ��� ����"];
                            createitem.Description += $"\n��� ȹ��� + {(createitem.mEffect.EquipRate * 100f).ToString()}%";
                            break;
                        case 6:
                            createitem.mEffect.Speed = (float)data[rate]["�̼�"];
                            createitem.Description += $"\n�̵��ӵ� + {(createitem.mEffect.Speed * 100f).ToString()}%";
                            break;
                        case 7:
                            createitem.mEffect.Mana = (int)data[rate]["����"];
                            createitem.Description += $"\n���� + {createitem.mEffect.Mana.ToString()}";
                            break;
                    }

                }
            }
            return createitem;
        }
    }
}
