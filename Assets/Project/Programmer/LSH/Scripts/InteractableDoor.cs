using UnityEngine;

public class InteractableDoor : MonoBehaviour
{
    [SerializeField] GameObject randomBox;
    [SerializeField] GameObject portal;
    GameObject[] monsterCount;

    [Range(20f, 50f)][SerializeField] float overlapSphereRadius; //�������� ��ó ����
    Collider[] hitColliders = new Collider[10]; //�ش� ��ü�� ��� ��� �ݶ��̴��� �־�� �迭
    [SerializeField] bool isInPlayer; //T���������Ǿ������� F�ƴ�
    [SerializeField] bool isMonsterAllDead; //T�������Ǹ��Ͱ����׾��� F�ƴ�

    private void Start()
    {
        monsterCount = GameObject.FindGameObjectsWithTag(Tag.Monster);
        randomBox.SetActive(false);
        portal.SetActive(false);
        isInPlayer = false;
        isMonsterAllDead = false;
    }
    private void Update()
    {
        if (isMonsterAllDead)
        {
            if (randomBox != null)
            {
                randomBox.SetActive(true);
            }
            if (isInPlayer)
            {
               // if (InputKey.GetButtonDown(InputKey.Interaction))
               // {
                    portal.SetActive(true);
               //}
            }

        }
    }

    private void FixedUpdate()
    {
        int detectedLayer = 0;
        detectedLayer |= 1 << Layer.Monster;
        detectedLayer |= 1 << Layer.Player;
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, overlapSphereRadius, hitColliders, detectedLayer);

        isMonsterAllDead = true;
        for (int i = 0; i < hitCount; i++)
        {
            if (hitColliders[i].gameObject.tag == Tag.Player)
            {
                isInPlayer = true;
            }
            else
            {
                isInPlayer = false;
            }


            if (hitColliders[i].gameObject.tag == Tag.Monster)
            {
                isMonsterAllDead = false;
            }
        }
        //foreach (Collider col in hitColliders)
        //{
        //    if (col.gameObject.tag != Tag.Monster)
        //    {
        //        isMonsterAllDead = true;
        //    }
        //    else
        //    {
        //        return;
        //    }

        //}

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, overlapSphereRadius);
    }

}
