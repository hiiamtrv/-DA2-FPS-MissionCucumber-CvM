using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class Model
    {
        float _interactTime;
    

        public Model(float interactTime)
        {
            this._interactTime = interactTime;
        }

        public float InteractTime { get => this._interactTime; set => this._interactTime = value; }
    }
}
