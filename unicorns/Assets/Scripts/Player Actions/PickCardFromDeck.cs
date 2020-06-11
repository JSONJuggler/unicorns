using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    [CreateAssetMenu(menuName = "Actions/Player Actions/PickCardFromDeck")]
    public class PickCardFromDeck : PlayerAction
    {
        public override void Execute(PlayerHolder player)
        {
            GameManager.singleton.PickNewCardFromDeck(player);
        }
    }
}