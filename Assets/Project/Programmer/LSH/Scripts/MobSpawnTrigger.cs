using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnTrigger : MonoBehaviour
{

    //[SerializeField] GameObject triggerObject;

    [SerializeField] GameObject monsterPrefabs;

    //Ʈ���� ������ ��ġ �� �ڱ��ڽ��� �ν�����â�� ����
    // Ʈ���ſ� �÷��̾ ������ �׶� ���� ����
    private void Awake()
    {
        monsterPrefabs.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Player)
        {
            monsterPrefabs.SetActive(true);
        }

    }


}
