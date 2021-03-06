﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unicorn.GameElements;

namespace unicorn
{
    [CreateAssetMenu(menuName = "Holders/Player Holder")]
    public class PlayerHolder : ScriptableObject
    {
        public string username;
        // public string[] startingCards;
        // public List<string> myDeck = new List<string>();

        // [System.NonSerialized]
        // public List<string> all_myDeckCards = new List<string>();

        [System.NonSerialized]
        public int photonId = -1;

        public bool isHumanPlayer;

        public GE_Logic handLogic;
        public GE_Logic downLogic;

        [System.NonSerialized]
        public CardHolders currentHolder;

        [System.NonSerialized]
        public List<CardInstance> handCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> cardsDown = new List<CardInstance>();
        [System.NonSerialized]
        public List<int> cardInstIds = new List<int>();

        // public List<Card> allCardInstances = new List<Card>();

        // local multiplayer player init for a players deck of cards

        // public void Init()
        // {
        //     all_myDeckCards.AddRange(myDeck);
        // }

        // public void CardToDiscardPile(CardInstance c)
        // {
        //     if (handCards.Contains(c))
        //         handCards.Remove(currentHolder);

        //     if (cardsDown.Contains(c))
        //         cardsDown.Remove(currentHolder);
        // }

        public void DropCard(CardInstance inst)
        {
            if (handCards.Contains(inst))
                handCards.Remove(inst);

            cardsDown.Add(inst);
        }
        // public List<CardInstance> resoucesList = new List<CardInstance>();
        // public int resourcesCount
        // {
        //     get { return resourcesGrid.value.GetComponentsInChildren<CardViz>().Length; }
        // }

        // public void AddResourceCard(GameObject cardObj)
        // {
        //     ResourceHolder resourceHolder = new ResourceHolder
        //     {
        //         cardObj = cardObj
        //     };

        //     resourcesList.Add(resourceHolder);
        // }

        // public int NonUsedCards()
        // {
        //     int result = 0;

        //     for (int i = 0; i < resoucesList.Count; i++)
        //     {
        //         if (!resourcesList[i].isUsed)
        //         {
        //             result++;
        //         }
        //     }
        // }

        // public bool CanUseCard(Card c)
        // {
        //     bool result = false;

        //     // logic here determining if card could be used
        // }
    }
}