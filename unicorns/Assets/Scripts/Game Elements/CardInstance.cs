using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    public class CardInstance : MonoBehaviour, IClickable
    {
        public CardViz viz;
        public unicorn.GameElements.GE_Logic currentLogic;

        void Start()
        {
            viz = GetComponent<CardViz>();
        }

        public void OnClick()
        {
            if (currentLogic == null)
                return;
            currentLogic.OnClick(this);
        }

        public void OnHighlight()
        {
            if (currentLogic == null)
                return;
            // Debug.Log("this card has logic");
            currentLogic.OnHighlight(this);
        }
    }
}