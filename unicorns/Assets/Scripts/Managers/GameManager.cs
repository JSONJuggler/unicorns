using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unicorn.GameStates;

namespace unicorn
{
    public class GameManager : MonoBehaviour
    {
        public PlayerHolder[] all_players;
        public PlayerHolder currentPlayer;
        public CardHolders playerOneHolder;
        public CardHolders otherPlayerHolder;
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

            SetupPlayers();

            CreateStartingCards();

            turnText.value = turns[turnIndex].player.username;
            onTurnChanged.Raise();
        }

        void SetupPlayers()
        {
            foreach (PlayerHolder p in all_players)
            {
                if (p.isHumanPlayer)
                {
                    p.currentHolder = playerOneHolder;
                }
                else
                {
                    p.currentHolder = otherPlayerHolder;
                }
            }
        }

        void CreateStartingCards()
        {
            ResourcesManager rm = Settings.GetResourcesManager();

            for (int p = 0; p < all_players.Length; p++)
            {
                for (int i = 0; i < all_players[p].startingCards.Length; i++)
                {
                    GameObject go = Instantiate(cardPrefab) as GameObject;
                    CardViz v = go.GetComponent<CardViz>();
                    v.LoadCard(rm.GetCardInstance(all_players[p].startingCards[i]));
                    CardInstance inst = go.GetComponent<CardInstance>();
                    inst.currentLogic = all_players[p].handLogic;
                    Settings.SetParentForCard(go.transform, all_players[p].currentHolder.handGrid.value);
                    // all_players[p].handCards.Add(inst);
                }
            }
        }

        // public bool switchPlayer;

        private void Update()
        {
            // if (switchPlayer)
            // {
            //     switchPlayer = false;

            //     playerOneHolder.LoadPlayer(all_players[0]);
            //     otherPlayerHolder.LoadPlayer(all_players[1]);
            // }
            bool isComplete = turns[turnIndex].Execute();

            if (isComplete)
            {
                turnIndex++;
                if (turnIndex > turns.Length - 1)
                {
                    turnIndex = 0;
                }

                turnText.value = turns[turnIndex].player.username;
                onTurnChanged.Raise();
            }

            if (currentState != null)
                currentState.Tick(Time.deltaTime);

        }

        public void SetState(State state)
        {
            currentState = state;
        }

        public void EndCurrentPhase()
        {
            turns[turnIndex].EndCurrentPhase();
        }
    }
}
