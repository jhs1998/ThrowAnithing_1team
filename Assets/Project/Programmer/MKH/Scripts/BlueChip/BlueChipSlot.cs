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
        public AdditionalEffect Effect { get { return mEffect; }  set { mEffect = value; } }

        private int listIndex;
        public int ListIndex { get { return listIndex; } set { listIndex = value; } }

        [Header("���Կ� �ִ� UI ������Ʈ")]
        [SerializeField] private Image mEffectImage;
        [SerializeField] TMP_Text nameText;
        [SerializeField] TMP_Text descriptionText;

        // ������ �̹��� ���� ����
        private void SetColor(float _alpha)
        {
            Color color = mEffectImage.color;
            color.a = _alpha;
            mEffectImage.color = color;
        }

        // ������ ����
        public void AddEffect(AdditionalEffect effect)
        {
            mEffect = effect;
            nameText.text = effect.Name;
            descriptionText.text = effect.Description;
            mEffectImage.sprite = mEffect.Image;
            SetColor(1);
        }

        // ������ ����
        public void ClearSlot()
        {
            mEffect = null;
            nameText.text = "-";
            descriptionText.text = "";
            mEffectImage.sprite = null;
        }

        // ���� ����
        public void SetSlot()
        {
            mEffectImage.sprite = null;
            nameText.text = "-";
            descriptionText.text = "";
        }
    }
}
