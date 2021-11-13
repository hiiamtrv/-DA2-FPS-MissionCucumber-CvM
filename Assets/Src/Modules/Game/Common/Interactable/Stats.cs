using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class Stats : MonoBehaviour
    {
        public float interactTime;
        public bool canMoveWhileInteract;

        Model _model;
        public Model Model => this._model;

        void Awake()
        {
            this._model = new Model(interactTime, canMoveWhileInteract);
        }

        public static float OBTAIN_TIME = 3;
    }
}