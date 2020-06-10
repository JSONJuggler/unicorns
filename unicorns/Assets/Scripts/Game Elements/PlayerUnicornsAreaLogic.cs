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
                Debug.Log("Placing card down");

                card.value.transform.SetParent(areaGrid.value.transform);
                card.value.transform.localPosition = Vector3.zero;
                card.value.transform.localScale = Vector3.one;
                card.value.transform.localEulerAngles = Vector3.zero;
                card.value.gameObject.SetActive(true);
                card.value.currentLogic = cardDownLogic;
                //Place card down
            }
        }
    }
}