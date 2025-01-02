using UnityEngine;

public class Forge : MonoBehaviour
{
    [SerializeField] GameObject upPopup;
    [SerializeField] GameObject _ui;

    public bool IsUIActive;
    public bool IsActive;
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
        Debug.Log($"{name} {InputKey.GetButtonDown(InputKey.PopUpClose)}");
        if (InputKey.GetButtonDown(InputKey.PopUpClose))
        {
            Debug.Log("닫음");
            if (_ui.activeSelf == true && IsUIActive == true)
            {
                Debug.Log("닫음");
                _ui.SetActive(false);
            }
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
}
