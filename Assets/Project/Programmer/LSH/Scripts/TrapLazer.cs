using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TrapLazer : MonoBehaviour
{
    
    [SerializeField] GameObject lazer;
    [SerializeField] Transform muzzle;

    [Range(5f, 10f)] [SerializeField] float count;
    WaitForSeconds blinkTiming;
    Coroutine blinkRoutine;
    public UnityAction triggerExit;

    private void Start()
    {
        blinkRoutine = StartCoroutine(LazerBlinkRoutine());
    }


    IEnumerator LazerBlinkRoutine()
    {
        while (true)
        {
            blinkTiming = new WaitForSeconds(count);
            lazer.SetActive(true);
            yield return blinkTiming;

            blinkTiming = new WaitForSeconds(count*0.5f);
            lazer.SetActive(false);
            triggerExit?.Invoke();
            yield return blinkTiming;
        }

    }
}
