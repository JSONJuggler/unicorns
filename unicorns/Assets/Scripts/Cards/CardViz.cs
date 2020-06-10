using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace unicorn
{
    public class CardViz : MonoBehaviour
    {
        public string title;
        public Image cardFront;
        public Image cardBack;
        public int quantity;

        public Card card;

        public void LoadCard(Card c)
        {
            if (c == null)
            {
                return;
            }

            card = c;

            c.cardType.OnSetType(this);
            c.cardDeck.OnSetType(this);

            title = c.cardTitle;
            cardFront.sprite = c.cardFront;
            cardBack.sprite = c.cardBack;
            quantity = c.quantity;

        }
    }
}