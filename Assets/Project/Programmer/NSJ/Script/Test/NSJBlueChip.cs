using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NSJBlueChip : MonoBehaviour
{
    [SerializeField] AdditionalEffect additionalEffect;
    [SerializeField] TMP_Text nameText;

    private void Start()
    {
        nameText.SetText(additionalEffect.Name.GetText());
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Player)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.AddAdditional(additionalEffect);
            Destroy(gameObject);
        }   
    }

    public void SetBlueChip(AdditionalEffect additional)
    {
        additionalEffect = additional;
        nameText.SetText(additionalEffect.Name.GetText());
    }
}
