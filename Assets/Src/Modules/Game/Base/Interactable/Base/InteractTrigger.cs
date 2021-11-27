using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class InteractTrigger : MonoBehaviour
    {
        [SerializeField] InteractEngine _behaviour;

        public void DoInteract(GameObject gameObject)
        {
            if (_behaviour) _behaviour.DoInteract(gameObject);
        }
    }
}
