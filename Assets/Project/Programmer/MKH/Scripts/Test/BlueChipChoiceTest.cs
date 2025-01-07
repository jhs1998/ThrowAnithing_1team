using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueChipChoiceTest : MonoBehaviour
{
    [SerializeField] GameObject choice;

    private TestBlueChip blueChip;

    [SerializeField] BlueChipPanel blueChipPanel;

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
                if (Input.GetKeyDown(KeyCode.E))
                {
                    choice.SetActive(false);
                    bool success = blueChipPanel.AcquireEffect(blueChip.Effect);

                    //Debug.Log(blueChip.Effect);
                    _addBlueChipRoutine = null;
                    Destroy(other.gameObject);
                    yield break;
                }
            }
            yield return null;
        }
    }
}
