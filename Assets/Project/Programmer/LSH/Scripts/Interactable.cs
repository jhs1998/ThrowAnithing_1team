using UnityEngine;

public class Interactable : MonoBehaviour
{

    [Range(2f, 5f)][SerializeField] float overlapSphereRadius; //�������� ��ó ����
    [Range(2f, 5f)][SerializeField] float playerAreaRadius; //�÷��̾� ��ó ���������ɶ� ��ó ����
    Collider[] hitColliders = new Collider[1]; //�ش� ��ü�� ��� ��� �ݶ��̴��� �־�� �迭
    Collider playerCollider; //���߿��� �÷��̾� �ݶ��̴��� ������ ����

    [SerializeField] bool isInSphere; //T���������Ǿ������� F�ƴ�
    [SerializeField] GameObject pressKeyUI;
    [SerializeField] GameObject pcText;
    [SerializeField] GameObject consoleText;

    [SerializeField] GameObject[] itemPrefabs;
    Vector3 itemRandomSpawnArea;

    [SerializeField] AudioClip bluechip; // ���Ĩ ���� ȿ����
    [SerializeField] GameObject createEffect; // ���Ĩ ���� ����Ʈ

    private void Start()
    {
        isInSphere = false;
        SetActivePopUpUI(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.Player)
        {
            Debug.Log("�÷��̾� ����");
        }
    }

    private void Update()
    {
        if (isInSphere)
        {
            if (InputKey.GetButtonDown(InputKey.Interaction))
            {
                SettingSpawnArea();
                DropRandomItems();
                SetFalseObject();
            }
        }
    }

    private void FixedUpdate()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, overlapSphereRadius, hitColliders, 1 << Layer.Player);
        if (hitCount != 0)
        {
            for(int i = 0; i < hitCount; i++)
            {

                playerCollider = hitColliders[i];
                isInSphere = true;
                SetActivePopUpUI(true);
            }
        }
        else
        {
            isInSphere = true;
            SetActivePopUpUI(false);
        }

    }


    //�������� ������ ���� ����
    public void SettingSpawnArea()
    {
        float range_x = playerCollider.bounds.size.x * playerAreaRadius;
        float range_z = playerCollider.bounds.size.z * playerAreaRadius;
        range_x = Random.Range((range_x / 2) * -1, range_x / 2);
        range_z = Random.Range((range_z / 2) * -1, range_z / 2);
        itemRandomSpawnArea = new Vector3(range_x, 0.5f, range_z);
    }

    //�������� ������ ������ ���� �� ����
    public void DropRandomItems()
    {
        int itemRandom = Random.Range(0, itemPrefabs.Length);
        Instantiate(itemPrefabs[itemRandom], transform.position, Quaternion.identity);
        if (itemPrefabs[0])
        {
            SoundManager.PlaySFX(bluechip);
            ObjectPool.GetPool(createEffect, itemPrefabs[0].transform.position, Quaternion.Euler(-90, 0, 0), 5f);
            Debug.Log(ObjectPool.GetPool(createEffect, transform.position, Quaternion.Euler(-90, 0, 0), 5f));
        }


    }

    //������ �������� �������� �����
    public void SetFalseObject()
    {
        Destroy(gameObject);
    }

    private void SetActivePopUpUI(bool isActive)
    {
        if (pressKeyUI == null)
            return;

        GameObject _pc = pcText;
        GameObject _console = consoleText;
        // �� ����̽��� �´� �ؽ�Ʈ Ȱ��ȭ
        _pc.SetActive(InputKey.PlayerInput.currentControlScheme == InputType.PC);
        _console.SetActive(InputKey.PlayerInput.currentControlScheme == InputType.CONSOLE);

        pressKeyUI.SetActive(isActive);
    }
}
