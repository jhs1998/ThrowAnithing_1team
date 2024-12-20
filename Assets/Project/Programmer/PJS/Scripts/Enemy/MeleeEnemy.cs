using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    private void Update()
    {
        if (playerObj == null)
        {
            playerObj = null;
            return;
        }
    }
}
