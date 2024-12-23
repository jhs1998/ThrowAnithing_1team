using UnityEngine;

public class BlueChipPanel : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject blueChipPanel;

    private void Awake()
    {
        blueChipPanel.SetActive(false);
    }

    private void Update()
    {
        TryOpenPanel();
    }

    private void TryOpenPanel()
    {
        if (inventory.activeSelf == true)
        {
            if (Input.GetKey(KeyCode.CapsLock))
            {
                OpenPanel();
            }
            else
            {
                ClosePanel();
            }
        }
    }

    private void OpenPanel()
    {
        blueChipPanel.SetActive(true);
    }

    private void ClosePanel()
    {
        blueChipPanel.SetActive(false);
    }
}
