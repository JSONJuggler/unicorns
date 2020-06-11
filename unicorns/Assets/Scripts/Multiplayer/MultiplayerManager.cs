using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    public class MultiplayerManager : Photon.MonoBehaviour
    {
        public static MultiplayerManager singleton;
        List<NetworkPrint> players = new List<NetworkPrint>();

        void OnPhotonInstantiate(PhotonMessageInfo info)
        {
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

        public void AddPlayer(NetworkPrint n_print)
        {
            players.Add(n_print);
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
    }
}