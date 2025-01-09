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
            _player.CantOperate = false;
        }
    }

    IEnumerator InteractorRoutine(Collider other)
    {
        while (true)
        {
            if (InputKey.GetButtonDown(InputKey.PrevInteraction))
            {
                Forge = other.GetComponent<Forge>();
                if (other.gameObject.tag != Tag.UnInteractable)
                {
                    if (IsInteractableActive == false)
                    {
                        _player.CantOperate = true;
                    }
                }

            }
            yield return null;
            if (Forge != null)
                IsInteractableActive = Forge.IsUIActive;
        }
    }
}
