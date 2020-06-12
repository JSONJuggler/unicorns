using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    [CreateAssetMenu(menuName = "Deck Profile")]
    public class DeckProfile : ScriptableObject
    {
        public string[] cardIds;
    }
}