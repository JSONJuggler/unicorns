using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace unicorn
{
    public class CardViz : MonoBehaviour
    {
        public string title;
        public string type;
        public string deck;
        public Image cardFront;
        public int quantity;

        public Card card;

        private void Start()
        {
            LoadCard(card);
        }

        public void LoadCard(Card c)
        {
            if (c == null)
            {
                return;
            }

            card = c;

            c.cardType.OnSetType(this);

            title = c.cardTitle;
            // type = c.cardType;
            deck = c.cardDeck;
            cardFront.sprite = c.cardFront;
            quantity = c.quantity;

        }
    }
}