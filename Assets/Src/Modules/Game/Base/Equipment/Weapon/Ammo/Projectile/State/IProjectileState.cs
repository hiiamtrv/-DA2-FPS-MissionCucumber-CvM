using UnityEngine;

namespace Projectile
{
    namespace State
    {
        public interface IProjectileState
        {
            public void OnCollsion();
        }
    }
}