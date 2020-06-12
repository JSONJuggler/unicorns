using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    [CreateAssetMenu(menuName = "Area/EnemyUnicornsWhenHoldingCard")]
    public class EnemyUnicornsAreaLogic : AreaLogic
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
                MultiplayerManager.singleton.PlayerWantsToUseCard(card.value.viz.card.instId, GameManager.singleton.localPlayer.photonId, MultiplayerManager.CardOperation.dropMagicalUnicornTypeEnemy);
            }
        }
    }
}