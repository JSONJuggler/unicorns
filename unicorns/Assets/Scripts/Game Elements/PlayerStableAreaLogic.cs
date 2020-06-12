using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    [CreateAssetMenu(menuName = "Area/PlayerStableWhenHoldingCard")]
    public class PlayerStableAreaLogic : AreaLogic
    {
        public CardVariable card;
        public CardType upgradeType;
        public CardType downgradeType;
        public SO.TransformVariable stableAreaGrid;
        public GameElements.GE_Logic cardDownLogic;

        public override void Execute()
        {
            if (card.value == null)
                return;

            if (card.value.viz.card.cardType == upgradeType || card.value.viz.card.cardType == downgradeType)
            {
                MultiplayerManager.singleton.PlayerWantsToUseCard(card.value.viz.card.instId, GameManager.singleton.localPlayer.photonId, MultiplayerManager.CardOperation.dropStableType);
            }
        }
    }
}