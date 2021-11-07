using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class Stats : MonoBehaviour
    {
        public float Speed;
        public float JumpHeight;
        public float Sensitivity;

        void Awake()
        {
            SPEED = this.Speed;
            JUMP_HEIGHT = this.JumpHeight;
            SENSITIVITY = this.Sensitivity;
        }

        public static float SPEED;
        public static float JUMP_HEIGHT;
        public static float SENSITIVITY;
    }
}