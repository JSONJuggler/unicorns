using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    public class MultiplayerManager : Photon.MonoBehaviour
    {
        #region Variables
        public static MultiplayerManager singleton;
        List<NetworkPrint> players = new List<NetworkPrint>();
        NetworkPrint localPlayer;
        Transform multiplayerReferences;

        public PlayerHolder localPlayerHolder;
        public PlayerHolder clientPlayerHolder;
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
            object[] data = new object[1];
            data[0] = profile.cardIds;

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

            foreach (NetworkPrint p in players)
            {
                if (p.isLocal)
                {
                    localPlayerHolder.photonId = p.photonId;
                    // localPlayerHolder.all_myDeckCards.Clear();
                    // localPlayerHolder.all_myDeckCards.AddRange(p.GetStartingCardIds());
                }
                else
                {
                    clientPlayerHolder.photonId = p.photonId;
                    // clientPlayerHolder.all_myDeckCards.Clear();
                    // clientPlayerHolder.all_myDeckCards.AddRange(p.GetStartingCardIds());
                }
            }

            if (NetworkManager.isMaster)
            {
                photonView.RPC("RPC_InitGame", PhotonTargets.All, 1);
            }
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
    }
}