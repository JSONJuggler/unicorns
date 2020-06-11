using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    [CreateAssetMenu(menuName = "Turns/ClientEmptyPhase")]
    public class ClientEmptyPhase : Phase
    {

        public override bool IsComplete()
        {
            if (forceExit)
            {
                forceExit = false;
                return true;
            }

            return false;
        }

        public override void OnEndPhase()
        {

        }

        public override void OnStartPhase()
        {

        }
    }
}
