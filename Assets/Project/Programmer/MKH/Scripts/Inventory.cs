using MKH;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [HideInInspector] InventoryController Controller;
    [HideInInspector] EquipmentInventory EquipInventory;
    [HideInInspector] InventoryMain InventoryMain;
    [HideInInspector] BlueChipPanel BlueChipPanel;
    [HideInInspector] BlueChipChoiceController BlueChipChoiceController;
    [HideInInspector] BlueChipChoicePanel BlueChipChoicePanel;
    [SerializeField] public GameObject BlueChipChoice;
    [SerializeField] public GameObject ChoicePanel;

    [Inject]
    PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        // 임시로 싱글톤 갈아끼우기 꼼수
        // TODO : 이후 블루칩 초기화 기능 구현후 제거 필요
        else if (SceneManager.GetActiveScene().name == SceneName.LobbyScene)
        {
            Destroy(Instance.gameObject);
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitGetComponent();


        playerData.Inventory.Inventory = EquipInventory;
        playerData.Inventory.InventoryMain = InventoryMain;
        playerData.Inventory.BlueChipChoice = BlueChipChoice;
        playerData.Inventory.BlueChipPanel = BlueChipPanel;
        playerData.Inventory.BlueChipChoicePanel = BlueChipChoicePanel;
        playerData.Inventory.ChoicePanel = ChoicePanel;
        playerData.Inventory.BlueChipChoiceController = BlueChipChoiceController;

    }

    private void InitGetComponent()
    {
        Controller = GetComponentInChildren<InventoryController>();
        EquipInventory = GetComponentInChildren<EquipmentInventory>();
        InventoryMain = GetComponentInChildren<InventoryMain>();
        BlueChipPanel = GetComponentInChildren<BlueChipPanel>();
        BlueChipChoicePanel = GetComponentInChildren<BlueChipChoicePanel>();
        BlueChipChoiceController = GetComponentInChildren<BlueChipChoiceController>();
    }

    private void InitSingleTon()
    {

    }
}
