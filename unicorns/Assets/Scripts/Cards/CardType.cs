using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    public abstract class CardType : ScriptableObject
    {
        public string typeName;

        public abstract void OnSetType(CardViz viz);
    }
}