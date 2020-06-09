using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    public class CurrentSelected : MonoBehaviour
    {
        public CardVariable currentCard;
        public CardViz cardViz;

        Transform mTransform;

        public void LoadCard()
        {
            if (currentCard.value == null)
                return;
            currentCard.value.gameObject.SetActive(false);
            cardViz.LoadCard(currentCard.value.viz.card);
            cardViz.gameObject.SetActive(true);
        }

        private void Start()
        {
            mTransform = this.transform;
        }

        void Update()
        {
            mTransform.position = Input.mousePosition;
        }
    }
}
