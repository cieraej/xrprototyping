using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaycastInteraction
{
    public class KeyInputRaycastInteraction : RaycastInteraction
    {
        [SerializeField] private KeyCode _key;

        private new void Update()
        {
            base.Update();

            if (Input.GetKeyDown(_key))
            {
                base.InputDown();
            }
            else if (Input.GetKeyUp(_key))
            {
                base.InputUp();
            }
        }

    }
}
