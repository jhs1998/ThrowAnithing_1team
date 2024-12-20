using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSH_PlayerModel : MonoBehaviour
{
    [SerializeField] private int _hp;
    public int HP { get { return _hp; } private set { } }

    Coroutine damageRoutine;
    WaitForSeconds count;

    private void Start()
    {
        _hp = 100;
        count = new WaitForSeconds(1f);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Lazer")
        {

            if (damageRoutine != null)
            {
                Debug.Log("¡¯¿‘2");
                TrapLazer lazer = other.gameObject.GetComponent<TrapLazer>();
                if(lazer.triggerExit == RoutineEnd)
                    lazer.triggerExit -= RoutineEnd;
            }
            else
            {
                Debug.Log("¡¯¿‘1");
                damageRoutine = StartCoroutine(LazerDamageRoutine());
            }
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Lazer")
        {

            if (other.gameObject.activeInHierarchy == false)
            {
                Debug.Log("≈ª√‚2");
                TrapLazer lazer = other.gameObject.GetComponent<TrapLazer>();
                lazer.triggerExit += RoutineEnd;
            }
            else
            {
                Debug.Log("≈ª√‚1");
                StopCoroutine(damageRoutine);
            }


            //if (damageRoutine != null)
            //{

                
            //}
            //else
            //{
                

            //}
        }

    }

    IEnumerator LazerDamageRoutine()
    {        
        while (true)
        {
            _hp -= 6;
            yield return count;
        }
    }

    public void RoutineEnd()
    {
        StopCoroutine(damageRoutine);
    }


}


