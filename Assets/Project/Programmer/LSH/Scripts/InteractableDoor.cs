using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour
{
    [SerializeField] GameObject randomBox;
    [SerializeField] GameObject portal;

    [Range(2f, 5f)][SerializeField] float overlapSphereRadius; //보물상자 근처 범위
    Collider[] hitColliders; //해당 물체에 닿는 모든 콜라이더를 넣어둘 배열
    [SerializeField] bool isInSphere; //T오버랩스피어에닿아있음 F아님


    private void Start()
    {
        portal.SetActive(false);
    }


    private void Update()
    {

        hitColliders = Physics.OverlapSphere(transform.position, overlapSphereRadius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == Tag.Player)
            {
                isInSphere = true;
            }
            else
            {
                isInSphere = false;
            }

        }


        if (isInSphere)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                portal.SetActive(true);
            }
        }



    }

    }
