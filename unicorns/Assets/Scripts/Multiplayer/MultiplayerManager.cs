using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    public class MultiplayerManager : Photon.MonoBehaviour
    {
        #region Variables
        public static MultiplayerManager singleton;

        Transform multiplayerReferences;

        public MainDataHolder dataHolder;

        // public PlayerHolder localPlayerHolder;
        // public PlayerHolder clientPlayerHolder;
        bool gameStarted;
        public bool countPlayers;
        GameManager gm
        {
            get
            {
                return GameManager.singleton;
            }
        }

        #endregion

        #region Player Management
        List<NetworkPrint> players = new List<NetworkPrint>();
        NetworkPrint localPlayer;
        NetworkPrint GetPlayer(int photonId)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].photonId == photonId)
                    return players[i];
            }
            return null;
        }
        #endregion

        #region Init
        void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            multiplayerReferences = new GameObject("references").transform;
            DontDestroyOnLoad(multiplayerReferences.gameObject);

            singleton = this;
            DontDestroyOnLoad(this.gameObject);

            InstantiateNetworkPrint();
            NetworkManager.singleton.LoadGameScene();
        }

        void InstantiateNetworkPrint()
        {
            PlayerProfile profile = Resources.Load("PlayerProfile") as PlayerProfile;
            DeckProfile gameDeck = Resources.Load("DeckProfile") as DeckProfile;
            object[] data = new object[1];
            // data[0] = profile.cardIds;
            data[0] = gameDeck.cardIds;

            PhotonNetwork.Instantiate("NetworkPrint", Vector3.zero, Quaternion.identity, 0, data);
        }
        #endregion

        #region Tick
        private void Update()
        {
            if (!gameStarted && countPlayers)
            {
                if (players.Count > 1)
                {
                    gameStarted = true;
                    StartMatch();
                }
            }
        }
        #endregion

        #region Starting the Match
        public void StartMatch()
        {
            ResourcesManager rm = gm.resourcesManager;

            if (NetworkManager.isMaster)
            {
                // BTW the master is always player 1! and photon.Id determines turn order eg 1 is first, 2 second, 3 third
                List<int> playerId = new List<int>();
                List<int> cardInstId = new List<int>();
                List<string> cardName = new List<string>();

                //treat this as the case for where local player is photon.Id == 1, aka network manager master aka room creator. all other local player conditionals will be handled in else statement
                foreach (NetworkPrint p in players)
                {
                    playerId.Add(p.photonId);

                    // conditional for if network print one is local
                    if (p.photonId == 1)
                    {
                        //turn order from the perspective of this local player 1->2->3 where 1 is local, 2 is client, and 3 is 3rd client
                        foreach (NetworkPrint z in players)
                        {
                            if (z.photonId == 1)
                            {
                                // here, first networkprint is local
                                z.playerHolder = gm.localPlayer;
                                z.playerHolder.photonId = z.photonId;

                            }
                            if (z.photonId == 2)
                            {
                                // here, second networkprint is client (second)
                                z.playerHolder = gm.clientPlayer;
                                z.playerHolder.photonId = z.photonId;

                            }
                            if (z.photonId == 3)
                            {
                                // here, third networkprint is third
                                z.playerHolder = gm.thirdrdClient;
                                z.playerHolder.photonId = z.photonId;
                            }
                        }
                    }

                    // // conditional for if network print two is local
                    // if (p.photonId == 2)
                    // {
                    //     //turn order from the perspective of this local player 2->3->1 where 2 is local, 3 is client, and 1 is 3rd client
                    //     foreach (NetworkPrint x in players)
                    //     {
                    //         if (x.photonId == 1)
                    //         {
                    //             // here, first networkprint is third
                    //             x.playerHolder = gm.thirdrdClient;
                    //             x.playerHolder.photonId = x.photonId;

                    //         }
                    //         if (x.photonId == 2)
                    //         {
                    //             // here, second networkprint is local
                    //             x.playerHolder = gm.localPlayer;
                    //             x.playerHolder.photonId = x.photonId;

                    //         }
                    //         if (x.photonId == 3)
                    //         {
                    //             // here, third networkprint is client (second)
                    //             x.playerHolder = gm.clientPlayer;
                    //             x.playerHolder.photonId = x.photonId;
                    //         }
                    //     }
                    // }

                    // // conditional for if networkprint three is local
                    // if (p.photonId == 3)
                    // {
                    //     //turn order from the perspective of this local player 3->1->2 where 3 is local, 1 is client, and 2 is 3rd client
                    //     foreach (NetworkPrint y in players)
                    //     {
                    //         if (y.photonId == 1)
                    //         {
                    //             // here, first networkprint is client (second)
                    //             y.playerHolder = gm.clientPlayer;
                    //             y.playerHolder.photonId = y.photonId;

                    //         }
                    //         if (y.photonId == 2)
                    //         {
                    //             // here, second networkprint is third 
                    //             y.playerHolder = gm.thirdrdClient;
                    //             y.playerHolder.photonId = y.photonId;

                    //         }
                    //         if (y.photonId == 3)
                    //         {
                    //             // here, third networkprint is local
                    //             y.playerHolder = gm.localPlayer;
                    //             y.playerHolder.photonId = y.photonId;
                    //         }
                    //     }
                    // }
                }

                foreach (string id in players[0].GetStartingCardIds())
                {
                    Card card = rm.GetCardInstance(id);
                    cardInstId.Add(card.instId);
                    cardName.Add(id);
                }

                for (int i = 0; i < playerId.Count; i++)
                {
                    for (int j = 0; j < cardInstId.Count; j++)
                    {
                        photonView.RPC("RPC_PlayerCreatesCard", PhotonTargets.All, playerId[i], cardInstId[j], cardName[j]);
                    }
                }

                photonView.RPC("RPC_InitGame", PhotonTargets.All, 1);
            }
            else
            {
                //handle all other local player conditionals here in this else statement
                foreach (NetworkPrint p in players)
                {
                    // conditional for if network print one is local
                    // if (p.photonId == 1)
                    // {
                    //     //turn order from the perspective of this local player 1->2->3 where 1 is local, 2 is client, and 3 is 3rd client
                    //     foreach (NetworkPrint z in players)
                    //     {
                    //         if (z.photonId == 1)
                    //         {
                    //             // here, first networkprint is local
                    //             z.playerHolder = gm.localPlayer;
                    //             z.playerHolder.photonId = z.photonId;

                    //         }
                    //         if (z.photonId == 2)
                    //         {
                    //             // here, second networkprint is client (second)
                    //             z.playerHolder = gm.clientPlayer;
                    //             z.playerHolder.photonId = z.photonId;

                    //         }
                    //         if (z.photonId == 3)
                    //         {
                    //             // here, third networkprint is third
                    //             z.playerHolder = gm.thirdrdClient;
                    //             z.playerHolder.photonId = z.photonId;
                    //         }
                    //     }
                    // }

                    // conditional for if network print two is local
                    if (p.photonId == 2)
                    {
                        //turn order from the perspective of this local player 2->3->1 where 2 is local, 3 is client, and 1 is 3rd client
                        foreach (NetworkPrint x in players)
                        {
                            if (x.photonId == 1)
                            {
                                // here, first networkprint is third
                                x.playerHolder = gm.thirdrdClient;
                                x.playerHolder.photonId = x.photonId;

                            }
                            if (x.photonId == 2)
                            {
                                // here, second networkprint is local
                                x.playerHolder = gm.localPlayer;
                                x.playerHolder.photonId = x.photonId;

                            }
                            if (x.photonId == 3)
                            {
                                // here, third networkprint is client (second)
                                x.playerHolder = gm.clientPlayer;
                                x.playerHolder.photonId = x.photonId;
                            }
                        }
                    }

                    // conditional for if networkprint three is local
                    if (p.photonId == 3)
                    {
                        //turn order from the perspective of this local player 3->1->2 where 3 is local, 1 is client, and 2 is 3rd client
                        foreach (NetworkPrint y in players)
                        {
                            if (y.photonId == 1)
                            {
                                // here, first networkprint is client (second)
                                y.playerHolder = gm.clientPlayer;
                                y.playerHolder.photonId = y.photonId;

                            }
                            if (y.photonId == 2)
                            {
                                // here, second networkprint is third 
                                y.playerHolder = gm.thirdrdClient;
                                y.playerHolder.photonId = y.photonId;

                            }
                            if (y.photonId == 3)
                            {
                                // here, third networkprint is local
                                y.playerHolder = gm.localPlayer;
                                y.playerHolder.photonId = y.photonId;
                            }
                        }
                    }
                }
            }
        }

        [PunRPC]
        public void RPC_PlayerCreatesCard(int photonId, int instId, string cardName)
        {
            Card c = gm.resourcesManager.GetCardInstance(cardName);
            c.instId = instId;

            NetworkPrint p = GetPlayer(photonId);
            p.AddCard(c);
        }

        [PunRPC]
        public void RPC_InitGame(int startingPlayer)
        {
            gm.isMultiplayer = true;
            gm.InitGame(startingPlayer);
        }

        public void AddPlayer(NetworkPrint n_print)
        {
            if (n_print.isLocal)
                localPlayer = n_print;

            players.Add(n_print);
            n_print.transform.parent = multiplayerReferences;
        }
        #endregion

        #region End Turn
        public void PlayerEndsTurn(int photonId)
        {
            photonView.RPC("RPC_PlayerEndsTurn", PhotonTargets.MasterClient, photonId);
        }

        [PunRPC]
        public void RPC_PlayerEndsTurn(int photonId)
        {
            if (photonId == gm.currentPlayer.photonId)
            {
                if (NetworkManager.isMaster)
                {
                    int targetId = gm.GetNextPlayerID();
                    photonView.RPC("RPC_PlayerStartsTurn", PhotonTargets.All, targetId);
                }
            }
        }

        [PunRPC]
        public void RPC_PlayerStartsTurn(int photonId)
        {
            gm.ChangeCurrentTurn(photonId);
        }
        #endregion

        #region Card Checks
        public void PlayerPicksCardFromDeck(PlayerHolder playerHolder)
        {
            NetworkPrint p = GetPlayer(playerHolder.photonId);

            if (p.deckCards.Count == 0)
            {
                // get all graveyard cards and reshuffle them into a new deck
                Debug.Log("shuffling deck");
                return;
            }

            Card c = p.deckCards[0];
            p.deckCards.RemoveAt(0);
            Debug.Log("attemtping to use card for player" + p.photonId);
            PlayerWantsToUseCard(c.instId, p.photonId, CardOperation.pickCardFromDeck);

            PlayerWantsToUseCard(c.instId, p.photonId, CardOperation.syncDeck);
        }

        public void PlayerWantsToUseCard(int cardInst, int photonId, CardOperation operation)
        {
            photonView.RPC("RPC_PlayerWantsToUseCard", PhotonTargets.MasterClient, cardInst, photonId, operation);
        }

        [PunRPC]
        public void RPC_PlayerWantsToUseCard(int cardInst, int photonId, CardOperation operation)
        {
            if (!NetworkManager.isMaster)
                return;

            bool hasCard = PlayerHasCard(cardInst, photonId);

            if (hasCard)
            {
                photonView.RPC("RPC_PlayerUsesCard", PhotonTargets.All, cardInst, photonId, operation);
            }
        }

        bool PlayerHasCard(int cardInst, int photonId)
        {
            NetworkPrint player = GetPlayer(photonId);
            Card c = player.GetCard(cardInst);
            return (c != null);
        }
        #endregion

        #region Card Operations
        public enum CardOperation
        {
            dropMagicalUnicornType, dropMagicalUnicornTypeEnemy, dropStableType, dropStableTypeEnemy, pickCardFromDeck, syncDeck
        }

        [PunRPC]
        public void RPC_PlayerUsesCard(int instId, int photonId, CardOperation operation)
        {
            NetworkPrint p = GetPlayer(photonId);
            Card card = p.GetCard(instId);

            switch (operation)
            {
                case CardOperation.dropStableType:
                    Debug.Log("Online Player placing stable down");
                    Settings.DropCard(card.cardPhysicalInst.transform, p.playerHolder.currentHolder.stableAreaGrid.value, card.cardPhysicalInst);
                    card.cardPhysicalInst.currentLogic = dataHolder.cardDownLogic;
                    card.cardPhysicalInst.gameObject.SetActive(true);
                    break;
                case CardOperation.dropStableTypeEnemy:
                    Debug.Log("Online Player placing enemy stable down");
                    Settings.DropCard(card.cardPhysicalInst.transform, p.playerHolder.currentHolder.enemyStableAreaGrid.value, card.cardPhysicalInst);
                    card.cardPhysicalInst.currentLogic = dataHolder.cardDownLogic;
                    card.cardPhysicalInst.gameObject.SetActive(true);
                    break;
                case CardOperation.dropMagicalUnicornType:
                    Debug.Log("Online Player placing unicorn down");
                    Settings.DropCard(card.cardPhysicalInst.transform, p.playerHolder.currentHolder.unicornAreaGrid.value, card.cardPhysicalInst);
                    card.cardPhysicalInst.currentLogic = dataHolder.cardDownLogic;
                    card.cardPhysicalInst.gameObject.SetActive(true);
                    break;
                case CardOperation.dropMagicalUnicornTypeEnemy:
                    Debug.Log("Online Player placing enemy unicorn down");
                    Settings.DropCard(card.cardPhysicalInst.transform, p.playerHolder.currentHolder.enemyUnicornAreaGrid.value, card.cardPhysicalInst);
                    card.cardPhysicalInst.currentLogic = dataHolder.cardDownLogic;
                    card.cardPhysicalInst.gameObject.SetActive(true);
                    break;
                case CardOperation.pickCardFromDeck:
                    GameObject go = Instantiate(dataHolder.cardPrefab) as GameObject;
                    CardViz v = go.GetComponent<CardViz>();
                    v.LoadCard(card);
                    card.cardPhysicalInst = go.GetComponent<CardInstance>();
                    card.cardPhysicalInst.currentLogic = dataHolder.handLogic;
                    Settings.SetParentForCard(go.transform, p.playerHolder.currentHolder.handGrid.value);
                    p.playerHolder.handCards.Add(card.cardPhysicalInst);
                    break;
                case CardOperation.syncDeck:
                    Debug.Log("player" + photonId + "drew" + instId);
                    foreach (NetworkPrint player in players)
                    {
                        if (player.photonId != photonId)
                        {
                            Debug.Log("removing" + instId + "for player" + player.photonId);
                            player.deckCards.RemoveAt(0);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
