using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    [CreateAssetMenu(fileName = "Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        [System.NonSerialized]
        public int instId;
        [System.NonSerialized]
        public CardViz cardViz;
        [System.NonSerialized]
        public CardInstance cardPhysicalInst;

        public CardType cardType;
        public CardDeck cardDeck;
        public string cardTitle;
        public Sprite cardFront;
        public Sprite cardBack;
        public int quantity;
    }
}


