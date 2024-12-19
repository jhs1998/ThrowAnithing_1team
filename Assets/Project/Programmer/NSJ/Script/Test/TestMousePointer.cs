using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Programmer.NSJ.RND.Script.Test
{
    public class TestMousePointer : MonoBehaviour
    {
        [SerializeField] private bool _isPointerInvisible;

        private void Start()
        {
            if (_isPointerInvisible == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}