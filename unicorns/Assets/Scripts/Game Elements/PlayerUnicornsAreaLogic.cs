using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    [CreateAssetMenu(menuName = "Area/PlayerUnicornsWhenHoldingCard")]
    public class PlayerUnicornsAreaLogic : AreaLogic
    {
        public CardVariable card;
        public CardType magicalUnicornType;
        public SO.TransformVariable areaGrid;
        public GameElements.GE_Logic cardDownLogic;

        public override void Execute()
        {
            if (card.value == null)
                return;

            if (card.value.viz.card.cardType == magicalUnicornType)
            {
                // bool canUse = Settings.gameManager.currentPlayer.CanUseCard(card.value.viz.card);
                // if (canUse)
                // {
                Debug.Log("Placing card down");

                Settings.DropCard(card.value.transform, areaGrid.value.transform, card.value);
                card.value.currentLogic = cardDownLogic;
                // }

                card.value.gameObject.SetActive(true);
            }
        }
    }
}