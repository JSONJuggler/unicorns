using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    public abstract class CardDeck : ScriptableObject
    {
        public string deckName;

        public abstract void OnSetType(CardViz viz);
    }
}