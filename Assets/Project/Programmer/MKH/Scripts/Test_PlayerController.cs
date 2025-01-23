using Assets.Project.Programmer.NSJ.RND.Script.Test.ZenjectTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed;
    [SerializeField] public GameObject drainfil;

    Vector3 m_Movement;

    private void Start()
    {
        drainfil.SetActive(false);
    }

    private void Update()
    {
        Move();
        OnDrainFil();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal"); // ���� �Է�
        float z = Input.GetAxis("Vertical"); // ���� �Է�

        m_Movement = new Vector3(x, 0, z).normalized; //����ȭ

        player.position += m_Movement * speed * Time.deltaTime;
    }

    private void OnDrainFil()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            drainfil.SetActive(true);
        }
        else
        {
            drainfil.SetActive(false);
        }
    }
}
