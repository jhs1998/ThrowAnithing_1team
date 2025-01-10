using MKH;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlueChipChoiceControlTest : MonoBehaviour
{
    [SerializeField] Button[] removeButtons;
    [SerializeField] GameObject popUp;
    [SerializeField] BlueChipPanel blueChipPanel;
    [SerializeField] BlueChipChoicePanelTest blueChipChoicePanel;
    [SerializeField] int popUpChoice;


    public void Canecel()
    {
        gameObject.SetActive(false);
    }

    public void Choice(int number)
    {
        for (int i = 0; i < blueChipPanel.mSlots.Length; i++)
        {
            if (blueChipPanel.mSlots[i].Effect == null)
            {
                blueChipPanel.mSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[number].Effect);
                blueChipChoicePanel.blueChipSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[number].Effect);

                gameObject.SetActive(false);

                return;
            }
        }
    }

    public void PopUp(int number)
    {
        popUp.SetActive(true);
        popUpChoice = number;
    }

    public void Remove()
    {
        blueChipPanel.mSlots[popUpChoice].ClearSlot();
        blueChipChoicePanel.blueChipSlots[popUpChoice].ClearSlot();
        popUp.SetActive(false);
        EventSystem.current.SetSelectedGameObject(blueChipChoicePanel.button.gameObject);
    }

    
    

    public void ClosePopUp()
    {
        popUp.SetActive(false);
        EventSystem.current.SetSelectedGameObject(blueChipChoicePanel.button.gameObject);
    }
}
