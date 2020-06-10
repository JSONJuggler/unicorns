using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    [CreateAssetMenu(menuName = "Holders/Card Holder")]
    public class CardHolders : ScriptableObject
    {
        public SO.TransformVariable handGrid;
        public SO.TransformVariable downGrid;

        public void LoadPlayer(PlayerHolder p)
        {
            foreach (CardInstance c in p.cardsDown)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, downGrid.value.transform);
            }

            foreach (CardInstance c in p.handCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, handGrid.value.transform);
            }
        }
    }
}