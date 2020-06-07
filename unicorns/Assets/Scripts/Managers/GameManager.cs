using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unicorn.GameStates;

namespace unicorn
{
    public class GameManager : MonoBehaviour
    {
        public State currentState;

        private void Update()
        {
            {
                currentState.Tick(Time.deltaTime);
            }
        }
    }
}
