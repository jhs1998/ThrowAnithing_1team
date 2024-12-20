using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainField : MonoBehaviour
{
    public  PlayerController _player;

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Trash")
        {
            Drain(other.transform);
        }
    }

    private void Drain(Transform trash)
    {
        if (_player.Model.CurThrowCount >= _player.Model.MaxThrowCount)
            return;
        trash.position = Vector3.MoveTowards(trash.position, _player.transform.position, 5f * Time.deltaTime);
    }
}
