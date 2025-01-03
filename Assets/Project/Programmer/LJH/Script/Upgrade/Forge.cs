using UnityEngine;

public class Forge : MonoBehaviour
{
    [SerializeField] GameObject upPopup;
    [SerializeField] GameObject _ui;

    public bool IsUIActive;
    public bool IsActive;
    PlayerController _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag(Tag.Player).GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Player))
        {
            upPopup.SetActive(true);
            IsActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Player))
        {
            
            upPopup.SetActive(false);
            _ui.SetActive(false);
            IsActive = false;
        }
    }

    private void Update()
    {
        if (IsActive == false)
        {
            return;
        }
        if (InputKey.GetButtonDown(InputKey.PopUpClose))
        {
            SetUnActiveUI();
        }

        //UI 활성화 됬는지 감지
        if (_ui.activeSelf == true)
        {
            IsUIActive = true;
        }
        else
        {
            IsUIActive = false;
        }
    }

    public void SetUnActiveUI()
    {
        _player.ChangeStateInteract(false);
        if (_ui.activeSelf == true && IsUIActive == true)
        {
            _ui.SetActive(false);
        }
    }
}


