using System.Collections;
using System.Collections.Generic;
using Cats.HealthState;
using Character;
using UnityEngine;

namespace Cats
{
    public class CatHealthEngine : HealthEngine
    {
        public override BaseState GetDefaultState() => new CatNormal(this);
    }
}
