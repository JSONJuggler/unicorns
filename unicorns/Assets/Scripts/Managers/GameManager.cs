using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unicorn.GameStates;

namespace unicorn
{
    public class GameManager : MonoBehaviour
    {
        [System.NonSerialized]
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

        public static GameManager singleton;

        public List<string> startingDeck = new List<string>();

        [System.NonSerialized]
        public List<string> all_cards = new List<string>();

        public void Awake()
        {
            singleton = this;

            all_players = new PlayerHolder[turns.Length];
            for (int i = 0; i < turns.Length; i++)
            {
                all_players[i] = turns[i].player;
            }

            currentPlayer = turns[0].player;
        }

        private void Start()
        {
            Settings.gameManager = this;

            all_cards.AddRange(startingDeck);

            SetupPlayers();

            // CreateStartingCards();

            turns[0].OnTurnStart();
            turnText.value = turns[turnIndex].player.username;
            onTurnChanged.Raise();
        }

        void SetupPlayers()
        {
            ResourcesManager rm = Settings.GetResourcesManager();

            for (int i = 0; i < all_players.Length; i++)
            {

                // if (all_players[i].isHumanPlayer)
                // {
                //     all_players[i].currentHolder = playerOneHolder;
                // }
                // else
                // {
                //     all_players[i].currentHolder = otherPlayerHolder;
                // }

                // this will only work with two players btq
                if (i == 0)
                {
                    all_players[i].currentHolder = playerOneHolder;
                }
                else
                {
                    all_players[i].currentHolder = otherPlayerHolder;
                }

                all_players[i].currentHolder.LoadPlayer(all_players[i]);
            }
        }

        // void CreateStartingCards()
        // {
        //     ResourcesManager rm = Settings.GetResourcesManager();

        //     for (int p = 0; p < all_players.Length; p++)
        //     {
        //         // for (int i = 0; i < all_players[p].startingCards.Length; i++)
        //         // {
        //         // GameObject go = Instantiate(cardPrefab) as GameObject;
        //         // CardViz v = go.GetComponent<CardViz>();
        //         // v.LoadCard(rm.GetCardInstance(all_players[p].startingCards[i]));
        //         // CardInstance inst = go.GetComponent<CardInstance>();
        //         // inst.currentLogic = all_players[p].handLogic;
        //         // Settings.SetParentForCard(go.transform, all_players[p].currentHolder.handGrid.value);
        //         // all_players[p].handCards.Add(inst);
        //         // }

        //         all_players[p].currentHolder.LoadPlayer(all_players[p]);
        //     }
        // }

        public void PickNewCardFromDeck(PlayerHolder p)
        {
            if (all_cards.Count == 0)
            {
                // get all graveyard cards and reshuffle them into a new deck
                Debug.Log("shuffling deck");
                return;
            }
            ResourcesManager rm = Settings.GetResourcesManager();

            string cardId = all_cards[0];
            all_cards.RemoveAt(0);
            GameObject go = Instantiate(cardPrefab) as GameObject;
            CardViz v = go.GetComponent<CardViz>();
            v.LoadCard(rm.GetCardInstance(cardId));
            CardInstance inst = go.GetComponent<CardInstance>();
            inst.currentLogic = p.handLogic;
            Settings.SetParentForCard(go.transform, p.currentHolder.handGrid.value);
            p.handCards.Add(inst);

        }

        public void LoadPlayerOnActive(PlayerHolder p)
        {
            PlayerHolder prevPlayer = playerOneHolder.playerHolder;
            LoadPlayerOnHolder(prevPlayer, otherPlayerHolder);
            LoadPlayerOnHolder(p, playerOneHolder);
        }

        public void LoadPlayerOnHolder(PlayerHolder p, CardHolders h)
        {
            h.LoadPlayer(p);
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

                // the current player has changed here
                currentPlayer = turns[turnIndex].player;
                turns[turnIndex].OnTurnStart();
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
