using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private PlayerController _player;

    Coroutine _interactorRoutine;
    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.Forge)
            return;
        if(_interactorRoutine == null)
        {
            Debug.Log(1);
            _interactorRoutine = StartCoroutine(InteractorRoutine(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != Layer.Forge)
            return;

        if (_interactorRoutine != null)
        {
            StopCoroutine(_interactorRoutine);
            _interactorRoutine = null;
        }
    }

    IEnumerator InteractorRoutine(Collider other)
    {
        while (true) 
        {
            if (Input.GetButtonDown(InputKey.Interaction))
            {
                _player.ChangeState(PlayerController.State.Interative);
            }
            yield return null;
        }
    }
}
