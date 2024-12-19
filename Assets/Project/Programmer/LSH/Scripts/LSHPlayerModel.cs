using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSHPlayerModel : MonoBehaviour
{
    [SerializeField] private int _hp;
    public int HP { get { return _hp; } private set { } }

    private void Start()
    {
        _hp = 100;
    }

}
