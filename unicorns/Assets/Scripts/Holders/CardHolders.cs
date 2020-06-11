﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    [CreateAssetMenu(menuName = "Holders/Card Holder")]
    public class CardHolders : ScriptableObject
    {
        public SO.TransformVariable handGrid;
        public SO.TransformVariable downGrid;

        [System.NonSerialized]
        public PlayerHolder playerHolder;

        public void LoadPlayer(PlayerHolder p)
        {
            if (p == null)
                return;

            playerHolder = p;
            p.currentHolder = this;

            foreach (CardInstance c in p.cardsDown)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, downGrid.value.transform);
            }

            foreach (CardInstance c in p.handCards)
            {
                if (c.viz != null)
                {
                    Settings.SetParentForCard(c.viz.gameObject.transform, handGrid.value.transform);
                }
            }
        }
    }
}