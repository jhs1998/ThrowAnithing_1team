using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestBlueChip : MonoBehaviour
{
    [SerializeField] public AdditionalEffect Effect;
    [SerializeField] TMP_Text nameText;

    private void Start()
    {
        nameText.SetText(Effect.Name.GetText());
    }

    public void SetBlueChip(AdditionalEffect additional)
    {
        Effect = additional;
        nameText.SetText(Effect.Name.GetText());
    }
}
