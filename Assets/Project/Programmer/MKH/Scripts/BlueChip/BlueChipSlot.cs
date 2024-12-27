using Assets.Project.Programmer.NSJ.RND.Script.Test.ZenjectTest;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    public class BlueChipSlot : MonoBehaviour
    {
        private AdditionalEffect mEffect;
        public AdditionalEffect Effect { get { return mEffect; } }

        [Header("슬롯에 있는 UI 오브젝트")]
        [SerializeField] private Image mEffectImage;

        [SerializeField] TMP_Text nameText;
        [SerializeField] TMP_Text descriptionText;

        // 아이템 이미지 투명도 조절
        private void SetColor(float _alpha)
        {
            Color color = mEffectImage.color;
            color.a = _alpha;
            mEffectImage.color = color;
        }

        // 아이템 습득
        public void AddEffect(AdditionalEffect effect)
        {
            mEffect = effect;
            nameText.text = effect.Name;
            descriptionText.text = effect.Description;
            SetColor(1);
        }

        // 아이템 제거
        public void ClearSlot()
        {
            mEffect = null;
            mEffectImage.sprite = null;
            nameText.text = null;
            descriptionText.text = null;
            SetColor(0);
        }
    }
}
