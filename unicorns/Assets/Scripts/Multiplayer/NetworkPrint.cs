using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    public class NetworkPrint : Photon.MonoBehaviour
    {
        public int photonId;
        public bool isLocal;

        string[] cardIds;
        public string[] GetStartingCardIds()
        {
            return cardIds;
        }

        public PlayerHolder playerHolder;

        Dictionary<int, Card> gameCards = new Dictionary<int, Card>();

        public List<Card> deckCards = new List<Card>();

        public void AddCard(Card c)
        {
            gameCards.Add(c.instId, c);
            deckCards.Add(c);
        }

        public Card GetCard(int instId)
        {
            Card c = null;
            gameCards.TryGetValue(instId, out c);
            return c;
        }

        void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            photonId = photonView.ownerId;
            isLocal = photonView.isMine;
            object[] data = photonView.instantiationData;

            cardIds = (string[])data[0];

            MultiplayerManager.singleton.AddPlayer(this);
        }
    }
}