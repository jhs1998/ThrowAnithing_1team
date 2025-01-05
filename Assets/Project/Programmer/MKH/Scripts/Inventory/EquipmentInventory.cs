using TMPro;
using UnityEngine;
using Zenject;

namespace MKH
{
    public class EquipmentInventory : InventoryBase
    {
        [Inject]
        public PlayerData playerData;

        [Header("현재 계산된 수치를 표현할 텍스트 라벨들")]
        [SerializeField] private TMP_Text mDamageLabel;
        [SerializeField] private TMP_Text mDefenseLabel;
        [SerializeField] private TMP_Text mHPLabel;
        [SerializeField] private TMP_Text mCriticalLabel;
        [SerializeField] private TMP_Text mAttackSpeedLabel;
        [SerializeField] private TMP_Text mSteminaLabel;
        [SerializeField] private TMP_Text mEquipRateLabel;
        [SerializeField] private TMP_Text mSpeedLabel;
        [SerializeField] private TMP_Text mManaLabel;

        private EquipmentEffect mCurrentEquipmentEffect;
        public EquipmentEffect CurrentEquipmentEffect { get { return mCurrentEquipmentEffect; } }

        new private void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            CalculateEffect();
            playerData.OnChangePlayerDataEvent += CalculateEffect;
        }
        public void CalculateEffect()
        {
            EquipmentEffect calcedEffect = new EquipmentEffect();

            foreach (InventorySlot slot in mSlots)
            {
                if (slot.Item == null) { continue; }

                calcedEffect += ((Item_Equipment)slot.Item).Effect;
            }

            mCurrentEquipmentEffect = calcedEffect;

            mDamageLabel.text = $"공격력 : {playerData.AttackPower.ToString()}";
            mDefenseLabel.text = $"방어력 : {playerData.Defense.ToString()}";
            mHPLabel.text = $"체력 : {playerData.MaxHp.ToString()}";
            mCriticalLabel.text = $"치명타 확률 : {playerData.CriticalChance.ToString()}%";
            mAttackSpeedLabel.text = $"공격속도 : {playerData.AttackSpeed.ToString()}";
            mSteminaLabel.text = $"스테미나 : {playerData.MaxStamina.ToString()}";
            mEquipRateLabel.text = $"장비 획득률 : {playerData.EquipmentDropUpgrade.ToString()}%";
            mSpeedLabel.text = $"이동속도 : {playerData.MoveSpeed.ToString()}";
            mManaLabel.text = $"마나 : {playerData.MaxMana.ToString()}";
        }

        public InventorySlot GetEquipmentSlot(ItemType type)
        {
            switch (type)
            {
                case ItemType.Helmet:
                    {
                        return mSlots[0];
                    }
                case ItemType.Shirts:
                    {
                        return mSlots[1];
                    }
                case ItemType.Glasses:
                    {
                        return mSlots[2];
                    }
                case ItemType.Gloves:
                    {
                        return mSlots[3];
                    }
                case ItemType.Pants:
                    {
                        return mSlots[4];
                    }
                case ItemType.Earring:
                    {
                        return mSlots[5];
                    }
                case ItemType.Ring:
                    {
                        return mSlots[6];
                    }
                case ItemType.Shoes:
                    {
                        return mSlots[7];
                    }
                case ItemType.Necklace:
                    {
                        return mSlots[8];
                    }
            }

            return null;
        }
    }
}
