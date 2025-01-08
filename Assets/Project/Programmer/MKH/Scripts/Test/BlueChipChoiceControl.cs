using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlueChipChoiceControl : MonoBehaviour
{
    [SerializeField] BlueChipPanel blueChipPanel;
    [SerializeField] BlueChipChoicePanelTest blueChipChoicePanel;

    [SerializeField] TestBlueChip blueChip;

    public void Canecel()
    {
        gameObject.SetActive(false);
    }

    public void Choice()
    {
        blueChip = gameObject.transform.GetComponent<TestBlueChip>();

        blueChipPanel.AcquireEffect(blueChip.Effect);
        blueChipChoicePanel.AcquireEffect(blueChip.Effect);
    }
}
