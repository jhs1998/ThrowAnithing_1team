using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    [Range(2f, 5f)] [SerializeField] float overlapSphereRadius; //보물상자 근처 범위
    [Range(2f, 5f)] [SerializeField] float playerAreaRadius; //플레이어 근처 랜덤생성될때 근처 범위
    Collider[] hitColliders; //해당 물체에 닿는 모든 콜라이더를 넣어둘 배열
    Collider playerCollider; //그중에서 플레이어 콜라이더만 저장할 변수

    [SerializeField] bool isInSphere; //T오버랩스피어에닿아있음 F아님
    [SerializeField] GameObject pressKeyUI;

    [SerializeField] GameObject[] itemPrefabs;
    Vector3 itemRandomSpawnArea;

    private void Start()
    {
        isInSphere = false;
        pressKeyUI.SetActive(false);
    }


    private void Update()
    {
        hitColliders = Physics.OverlapSphere(transform.position, overlapSphereRadius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == Tag.Player)
            {
                playerCollider = hitColliders[i];
                isInSphere = true;
                pressKeyUI.SetActive(true);
            }
            else
            {
                isInSphere = false;
                pressKeyUI.SetActive(false);
            }

        }

        if (isInSphere)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SettingSpawnArea();
                DropRandomItems();
                SetFalseObject(); 
            }
        }

    }


    //랜덤으로 스폰될 영역 설정
    public void SettingSpawnArea()
    {
        float range_x = playerCollider.bounds.size.x * playerAreaRadius;
        float range_z = playerCollider.bounds.size.z * playerAreaRadius;
        range_x = Random.Range((range_x / 2) * -1, range_x / 2);
        range_z = Random.Range((range_z / 2) * -1, range_z / 2);
        itemRandomSpawnArea = new Vector3(range_x, 0.5f, range_z);
    }

    //랜덤으로 스폰될 아이템 설정 후 생성
    public void DropRandomItems()
    {        
        int itemRandom = Random.Range(0, itemPrefabs.Length);
        Instantiate(itemPrefabs[itemRandom], itemRandomSpawnArea, Quaternion.identity);


    }

    //생성시 보물상자 아이템은 사라짐
    public void SetFalseObject()
    {
        gameObject.SetActive(false);
    }
}
