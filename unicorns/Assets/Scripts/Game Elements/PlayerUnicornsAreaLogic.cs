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
        public SO.TransformVariable unicornAreaGrid;
        public GameElements.GE_Logic cardDownLogic;

        public override void Execute()
        {
            if (card.value == null)
                return;

            if (card.value.viz.card.cardType == magicalUnicornType)
            {
                MultiplayerManager.singleton.PlayerWantsToUseCard(card.value.viz.card.instId, GameManager.singleton.localPlayer.photonId, MultiplayerManager.CardOperation.dropMagicalUnicornType);
                // bool canUse = Settings.gameManager.currentPlayer.CanUseCard(card.value.viz.card);
                // if (canUse)
                // {
                // Debug.Log("Placing card down");

                // Settings.DropCard(card.value.transform, unicornAreaGrid.value.transform, card.value);
                // card.value.currentLogic = cardDownLogic;
                // }

                // card.value.gameObject.SetActive(true);
            }
        }
    }
}