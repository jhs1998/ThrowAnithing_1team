using System.Collections;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public Forge Forge;
    public bool IsInteractableActive;
    public bool IsInteractive;
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
        if (_interactorRoutine == null)
        {
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
            _player.ChangeStateInteract(false);
        }
    }

    IEnumerator InteractorRoutine(Collider other)
    {
        while (true)
        {
            if (InputKey.GetButtonDown(InputKey.Interaction))
            {        
                Forge = other.GetComponent<Forge>();
                if (other.gameObject.tag != Tag.UnInteractable)
                {
                    if(IsInteractableActive == false && IsInteractive ==false)
                    {
                        _player.ChangeStateInteract(true);
                        IsInteractive = true;
                    }
                }



            }
            yield return null;
            if (Forge != null)
                IsInteractableActive = Forge.IsUIActive;
        }
    }
}
