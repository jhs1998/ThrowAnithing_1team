using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BlueChipChoice : MonoBehaviour
{
    [Inject]
    private PlayerData playerData;

    [SerializeField] GameObject choice;
    [SerializeField] GameObject choicePanel;

    private TestBlueChip blueChip;

    [SerializeField] BlueChipPanel blueChipPanel;
    PlayerController _player;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
        blueChipPanel = playerData.Inventory.BlueChipPanel;
        choice = playerData.Inventory.BlueChipChoice;
        choicePanel = playerData.Inventory.ChoicePanel;
    }
    private void Start()
    {       
        choice.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == Tag.BlueChip)
        {
            choice.SetActive(true);
            if(_addBlueChipRoutine == null)
            {
                _addBlueChipRoutine = StartCoroutine(AddBlueChipRoutine(other));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == Tag.BlueChip)
        {
            choice.SetActive(false);
            if(_addBlueChipRoutine != null)
            {
                StopCoroutine(_addBlueChipRoutine);
                _addBlueChipRoutine = null;
            }
        }
    }

    Coroutine _addBlueChipRoutine;
    IEnumerator AddBlueChipRoutine(Collider other)
    {
        blueChip = other.transform.GetComponent<TestBlueChip>();
        while (true)
        {
            if (other.gameObject.tag == Tag.BlueChip)
            {
                if (InputKey.GetButtonDown(InputKey.Interaction))
                {
                    choice.SetActive(false);
                    choicePanel.SetActive(true);
                    InputKey.SetActionMap(InputType.UI);
                    //bool success = blueChipPanel.AcquireEffect(blueChip.Effect);

                    //// 블루칩 플레이어 적용
                    //if (success == true)
                    //{
                    //    _player.AddAdditional(blueChip.Effect);
                    //}

                    _addBlueChipRoutine = null;
                    Destroy(other.gameObject);
                    yield break;
                }
            }
            yield return null;
        }
    }
}
