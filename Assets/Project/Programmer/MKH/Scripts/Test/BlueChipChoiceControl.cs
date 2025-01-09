using MKH;
using UnityEngine;

public class BlueChipChoiceControl : MonoBehaviour
{
    [SerializeField] BlueChipPanel blueChipPanel;
    [SerializeField] BlueChipChoicePanelTest blueChipChoicePanel;

    public void Canecel()
    {
        gameObject.SetActive(false);
    }

    public void Choice0()
    {
        for (int i = 0; i < blueChipPanel.mSlots.Length; i++)
        {
            if (blueChipPanel.mSlots[i].Effect == null)
            {
                blueChipPanel.mSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[0].Effect);
                blueChipChoicePanel.blueChipSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[0].Effect);

                gameObject.SetActive(false);

                return;
            }
        }
    }

    public void Choice1()
    {
        for (int i = 0; i < blueChipPanel.mSlots.Length; i++)
        {
            if (blueChipPanel.mSlots[i].Effect == null)
            {
                blueChipPanel.mSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[1].Effect);
                blueChipChoicePanel.blueChipSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[1].Effect);

                gameObject.SetActive(false);
                return;
            }
        }
    }

    public void Choice2()
    {
        for (int i = 0; i < blueChipPanel.mSlots.Length; i++)
        {
            if (blueChipPanel.mSlots[i].Effect == null)
            {
                blueChipPanel.mSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[2].Effect);
                blueChipChoicePanel.blueChipSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[2].Effect);

                gameObject.SetActive(false);
                return;
            }
        }
    }

    public void Remove0()
    {
        blueChipPanel.mSlots[0].ClearSlot();
        blueChipChoicePanel.blueChipSlots[0].ClearSlot();
    }

    public void Remove1()
    {
        blueChipPanel.mSlots[1].ClearSlot();
        blueChipChoicePanel.blueChipSlots[1].ClearSlot();
    }

    public void Remove2()
    {
        blueChipPanel.mSlots[2].ClearSlot();
        blueChipChoicePanel.blueChipSlots[2].ClearSlot();
    }

    public void Remove3()
    {
        blueChipPanel.mSlots[3].ClearSlot();
        blueChipChoicePanel.blueChipSlots[3].ClearSlot();
    }

    public void Remove4()
    {
        blueChipPanel.mSlots[4].ClearSlot();
        blueChipChoicePanel.blueChipSlots[4].ClearSlot();
    }
}
