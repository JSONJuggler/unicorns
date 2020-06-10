using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using unicorn;
using SO.UI;

namespace SO
{
    public class UpdateTextFromPhase : UIPropertyUpdater
    {
        public PhaseVariable currentPhase;
        public Text targetText;

        public override void Raise()
        {
            targetText.text = currentPhase.value.phaseName;
        }
    }
}