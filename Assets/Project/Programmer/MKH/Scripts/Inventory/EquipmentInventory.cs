using TMPro;
using UnityEngine;
using Zenject;

namespace MKH
{
    public class EquipmentInventory : InventoryBase
    {
        [Inject]
        public PlayerData playerData;

        public static bool IsInventoryActive = false;

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

            mDamageLabel.text = "Damage : " + playerData.AttackPower.ToString();
            mDefenseLabel.text = "Defense : " + playerData.Defense.ToString();
            mHPLabel.text = "HP : " + playerData.MaxHp.ToString();
            mCriticalLabel.text = "Critical : " + playerData.CriticalChance.ToString();
            mAttackSpeedLabel.text = "AttackSpeed : " + playerData.AttackSpeed.ToString("F2");
            mSteminaLabel.text = "Stemina : " + playerData.MaxStamina.ToString();
            mEquipRateLabel.text = "EquipRate : " + playerData.EquipmentDropUpgrade.ToString("F2");
            mSpeedLabel.text = "MoveSpeed : " + playerData.MoveSpeed.ToString("F2");
            mManaLabel.text = "Mana : " + playerData.MaxMana.ToString();
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
        private void Update()
        {
            TryOpenInventory();
        }

        private void TryOpenInventory()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!IsInventoryActive)
                    OpenInventory();
                else
                    CloseInventory();
            }
        }

        private void OpenInventory()
        {
            mInventoryBase.SetActive(true);
            IsInventoryActive = true;
        }

        private void CloseInventory()
        {
            mInventoryBase.SetActive(false);
            IsInventoryActive = false;
        }
    }
}
