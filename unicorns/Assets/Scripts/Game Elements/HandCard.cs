﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn.GameElements
{
    [CreateAssetMenu(menuName = "Game Elements/My Hand Card")]
    public class HandCard : GE_Logic
    {
        public SO.GameEvent onCurrentCardSelected;
        public CardVariable currentCard;
        public unicorn.GameStates.State holdingCard;

        public override void OnClick(CardInstance inst)
        {
            // Debug.Log("this card is on my hand");
            currentCard.Set(inst);
            Settings.gameManager.SetState(holdingCard);
            onCurrentCardSelected.Raise();
        }

        public override void OnHighlight(CardInstance inst)
        {
            // Debug.Log("this card has the logic of being in my hand");
        }
    }
}
