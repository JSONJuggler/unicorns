using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unicorn.GameStates;

namespace unicorn
{
    public class GameManager : MonoBehaviour
    {
        public ResourcesManager resourcesManager;
        public bool isMultiplayer;
        [System.NonSerialized]
        public PlayerHolder[] all_players;

        public PlayerHolder currentPlayer;

        public PlayerHolder localPlayer;
        public PlayerHolder clientPlayer;
        public CardHolders playerOneHolder;
        public CardHolders otherPlayerHolder;
        public State currentState;
        public GameObject cardPrefab;

        public int turnIndex;
        public Turn[] turns;
        public SO.GameEvent onTurnChanged;
        public SO.GameEvent onPhaseChanged;
        public SO.StringVariable turnText;

        public SO.TransformVariable discardPile;
        public List<CardInstance> discardPileCards = new List<CardInstance>();

        bool isInit;

        public static GameManager singleton;

        // public List<string> startingDeck = new List<string>();

        // [System.NonSerialized]
        // public List<string> all_cards = new List<string>();

        public void Awake()
        {
            Settings.gameManager = this;

            singleton = this;
        }

        public void InitGame(int startingPlayer)
        {
            all_players = new PlayerHolder[turns.Length];
            Turn[] _turns = new Turn[2];

            for (int i = 0; i < turns.Length; i++)
            {
                all_players[i] = turns[i].player;
                if (all_players[i].photonId == startingPlayer)
                {
                    _turns[0] = turns[i];
                    // currentPlayer = all_players[i];
                }
                else
                {
                    _turns[1] = turns[i];
                }
            }

            turns = _turns;

            // currentPlayer = turns[0].player;

            // all_cards.AddRange(startingDeck);

            SetupPlayers();

            // CreateStartingCards();

            turns[0].OnTurnStart();
            turnText.value = turns[turnIndex].player.username;
            onTurnChanged.Raise();
            isInit = true;
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
            MultiplayerManager.singleton.PlayerPicksCardFromDeck(p);
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
            if (!isInit)
                return;
            // if (switchPlayer)
            // {
            //     switchPlayer = false;

            //     playerOneHolder.LoadPlayer(all_players[0]);
            //     otherPlayerHolder.LoadPlayer(all_players[1]);
            // }
            bool isComplete = turns[turnIndex].Execute();

            if (!isMultiplayer)
            {
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
            }
            else
            {
                if (isComplete)
                {
                    MultiplayerManager.singleton.PlayerEndsTurn(currentPlayer.photonId);
                }
            }

            if (currentState != null)
                currentState.Tick(Time.deltaTime);
        }

        public int GetNextPlayerID()
        {
            int r = turnIndex;

            r++;
            if (r > turns.Length - 1)
            {
                r = 0;
            }

            return turns[r].player.photonId;
        }

        int GetPlayerTurnIndex(int photonId)
        {
            for (int i = 0; i < turns.Length; i++)
            {
                if (turns[i].player.photonId == photonId)
                    return i;
            }

            return -1;
        }

        public void ChangeCurrentTurn(int photonId)
        {
            turnIndex = GetPlayerTurnIndex(photonId);
            currentPlayer = turns[turnIndex].player;
            turns[turnIndex].OnTurnStart();
            turnText.value = turns[turnIndex].player.username;
            onTurnChanged.Raise();
        }

        public void SetState(State state)
        {
            currentState = state;
        }

        public void EndCurrentPhase()
        {
            if (currentPlayer.isHumanPlayer)
            {
                turns[turnIndex].EndCurrentPhase();
            }
        }

        public void PutCardToDiscardPile(CardInstance c)
        {
            discardPileCards.Add(c);
            c.transform.parent = discardPile.value;
            Vector3 p = Vector3.zero;

            p.x = discardPileCards.Count * 10;
            p.z = discardPileCards.Count * 10;
            c.transform.localPosition = p;
            c.transform.localRotation = Quaternion.identity;
            c.transform.localScale = Vector3.one;
        }
    }
}
