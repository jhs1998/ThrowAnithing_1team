using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEngine;

public class TrapStalactite : MonoBehaviour
{
    // ���̷� �˻��Ҷ� ����
    //[SerializeField] GameObject ray;
    //RaycastHit hit;
    //[SerializeField] float maxDistance = 15f;
    
    //���̸� ����ִٰ� or �ڽ� ���� �ȿ�
    //�÷��̾ ������ ���� ����
    //���ô� �������� �����ϸ� 2�ʵڿ� �����
    //���ô� �÷��̾�� �ε����� ü�� -40�����

    [SerializeField] GameObject stalactite;
    [SerializeField] GameObject boxArea;
    [SerializeField] GameObject particle;

    [SerializeField] Transform stalactiteBeforeMove;
    [SerializeField] Transform stalactiteAfterMove;

    Coroutine deleteRoutine;
    WaitForSeconds wait;
    [SerializeField] float deleteCount;


    // ���̷� �˻��ϴ� ����
    //private void Update()
    //{
    //    Debug.DrawRay(ray.transform.position, Vector3.down, Color.red);
    //    Debug.DrawLine(ray.transform.position, Vector3.down, Color.red);
    //    if (Physics.Raycast(ray.transform.position, Vector3.down, out hit, maxDistance))
    //    {
    //        if (hit.collider.gameObject.tag == Tag.Player)
    //        {
    //            stalactite.transform.position = 
    //                Vector3.MoveTowards(stalactiteBeforeMove.position, stalactiteAfterMove.position, 5f);
    //        }

    //    }


    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Player)
        {
            stalactite.GetComponent<Rigidbody>().isKinematic = false;
            Coroutine deleteRoutine = StartCoroutine(DeleteRoutine());
        }
    }

    
    IEnumerator DeleteRoutine()
    {
        particle.SetActive(false);
        wait = new WaitForSeconds(deleteCount);
        yield return wait;
        stalactite.SetActive(false);
    }

}
