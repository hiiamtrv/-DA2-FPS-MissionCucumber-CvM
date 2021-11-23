using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class Utils
    {
        public static bool IsFloating(CharacterController controller)
            => controller.collisionFlags == CollisionFlags.None;

        public static bool IsTouchHead(CharacterController controller)
            => (controller.collisionFlags & CollisionFlags.Above) != 0;

        public static bool IsOnlyTouchHead(CharacterController controller)
            => controller.collisionFlags == CollisionFlags.Above;

        public static bool IsTouchFoot(CharacterController controller)
            => (controller.collisionFlags & CollisionFlags.Below) != 0;

        public static bool IsOnlyTouchFoot(CharacterController controller)
            => controller.collisionFlags == CollisionFlags.Below;

        public static bool IsTouchSide(CharacterController controller)
            => (controller.collisionFlags & CollisionFlags.Sides) != 0;

        public static bool IsOnlyTouchSide(CharacterController controller)
            => controller.collisionFlags == CollisionFlags.Sides;

        public static (float, float) AdjustXZ(float x, float z)
        {
            //Balance values between moveX and moveZ: X and Z must cause the player has the speed below speed * 1
            Vector2 vtMove = new Vector2(x, z);
            if (vtMove.magnitude > 1)
            {
                vtMove = vtMove.normalized;
                x = vtMove.x;
                z = vtMove.y;
            }
            return (x, z);
        }
    }
}