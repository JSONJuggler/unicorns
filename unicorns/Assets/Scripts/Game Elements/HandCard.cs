using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn.GameElements
{
    [CreateAssetMenu(menuName = "Game Elements/My Hand Card")]
    public class HandCard : GE_Logic
    {
        public override void OnClick(CardInstance inst)
        {
            Debug.Log("this card is on my hand");
        }

        public override void OnHighlight(CardInstance inst)
        {

        }
    }
}
