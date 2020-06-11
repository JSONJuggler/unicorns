using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    public class EventManager : MonoBehaviour
    {
        #region My Calls
        public void CardIsDroppedDown(int instId, int ownerId)
        {
            NetworkManager.singleton.GetCard(instId, ownerId);
        }

        public void CardIsPickedUpFromDeck(int instId, int ownerId)
        {
            NetworkManager.singleton.GetCard(instId, ownerId);
        }
        #endregion
    }
}
