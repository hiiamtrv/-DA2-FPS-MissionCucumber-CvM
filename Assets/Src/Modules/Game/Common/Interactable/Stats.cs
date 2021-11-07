using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class Stats : MonoBehaviour
    {
        public float obtainTime;

        void Awake()
        {
            OBTAIN_TIME = this.obtainTime;
        }

        public static float OBTAIN_TIME = 3;
    }
}