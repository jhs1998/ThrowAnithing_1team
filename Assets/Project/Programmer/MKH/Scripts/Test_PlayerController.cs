using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Test_PlayerController : MonoBehaviour
{
    [SerializeField] float speed;

    Vector3 m_Movement;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal"); // 수평 입력
        float z = Input.GetAxis("Vertical"); // 수직 입력

        m_Movement = new Vector3(x, 0, z).normalized; //정규화

        transform.position += m_Movement * speed * Time.deltaTime;
    }
}
