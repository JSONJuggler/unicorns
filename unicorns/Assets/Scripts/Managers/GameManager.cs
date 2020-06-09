using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unicorn.GameStates;

namespace unicorn
{
    public class GameManager : MonoBehaviour
    {
        public State currentState;

        private void Start()
        {
            Settings.gameManager = this;
        }

        private void Update()
        {
            {
                currentState.Tick(Time.deltaTime);
            }
        }

        public void SetState(State state)
        {
            currentState = state;
        }
    }
}
