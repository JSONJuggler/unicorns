using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn.GameElements
{

    [CreateAssetMenu(menuName = "Game Elements/My Card On Table")]
    public class MyCardOnTable : GE_Logic
    {
        public SO.GameEvent onCurrentCardSelected;
        public CardVariable currentCard;
        public unicorn.GameStates.State holdingCard;

        public override void OnClick(CardInstance inst)
        {
            // Debug.Log("this card is mines and on the table");
            currentCard.Set(inst);
            Settings.gameManager.SetState(holdingCard);
            onCurrentCardSelected.Raise();
        }

        public override void OnHighlight(CardInstance inst)
        {
            // Debug.Log("this card has log of being mines and on the table");
        }
    }
}
