using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NSJBlueChipRemover : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] TMP_Text nameText;

    private void Start()
    {
        nameText.SetText($"Remove {index}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Player)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if(player.Model.AdditionalEffects.Count > index)
            {
                player.RemoveAdditional(player.Model.AdditionalEffects[index]);
            }
            
            // Destroy(gameObject);
        }
    }
}
