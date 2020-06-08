using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn.GameElements
{

    [CreateAssetMenu(menuName = "Game Elements/My Card On Table")]
    public class MyCardOnTable : GE_Logic
    {
        public override void OnClick(CardInstance inst)
        {
            Debug.Log("this card is mines and on the table");
        }

        public override void OnHighlight(CardInstance inst)
        {
            Debug.Log("this card has log of being mines and on the table");
        }
    }
}
