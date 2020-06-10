using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unicorn.GameElements;

namespace unicorn
{
    [CreateAssetMenu(menuName = "Holders/Player Holder")]
    public class PlayerHolder : ScriptableObject
    {
        public string[] startingCards;
        public SO.TransformVariable handGrid;
        public SO.TransformVariable downGrid;

        public GE_Logic handLogic;
        public GE_Logic downLogic;

        [System.NonSerialized]
        public List<CardInstance> handCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> cardsDown = new List<CardInstance>();
    }
}