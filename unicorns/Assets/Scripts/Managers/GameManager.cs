using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unicorn.GameStates;

namespace unicorn
{
    public class GameManager : MonoBehaviour
    {
        public PlayerHolder currentPlayer;
        public State currentState;
        public GameObject cardPrefab;

        public int turnIndex;
        public Turn[] turns;
        public SO.GameEvent onTurnChanged;
        public SO.GameEvent onPhaseChanged;
        public SO.StringVariable turnText;

        private void Start()
        {
            Settings.gameManager = this;
            CreateStartingCards();

            turnText.value = turns[turnIndex].turnName;
            onTurnChanged.Raise();
        }

        void CreateStartingCards()
        {
            ResourcesManager rm = Settings.GetResourcesManager();
            // Debug.Log(rm);
            for (int i = 0; i < currentPlayer.startingCards.Length; i++)
            {
                GameObject go = Instantiate(cardPrefab) as GameObject;
                CardViz v = go.GetComponent<CardViz>();
                v.LoadCard(rm.GetCardInstance(currentPlayer.startingCards[i]));
                CardInstance inst = go.GetComponent<CardInstance>();
                inst.currentLogic = currentPlayer.handLogic;
                Settings.SetParentForCard(go.transform, currentPlayer.handGrid.value);
            }
        }

        private void Update()
        {
            bool isComplete = turns[turnIndex].Execute();

            if (isComplete)
            {
                turnIndex++;
                if (turnIndex > turns.Length - 1)
                {
                    turnIndex = 0;
                }

                turnText.value = turns[turnIndex].turnName;
                onTurnChanged.Raise();
            }

            if (currentState != null)
                currentState.Tick(Time.deltaTime);

        }

        public void SetState(State state)
        {
            currentState = state;
        }
    }
}
