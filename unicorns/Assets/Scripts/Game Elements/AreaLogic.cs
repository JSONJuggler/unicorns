using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    public abstract class AreaLogic : ScriptableObject
    {
        public abstract void Execute();
    }
}