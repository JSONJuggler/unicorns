using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    [CreateAssetMenu(fileName = "Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public CardType cardType;
        public string cardTitle;
        public Sprite cardFront;
        // public string cardType;
        public string cardDeck;
        public int quantity;
    }
}


