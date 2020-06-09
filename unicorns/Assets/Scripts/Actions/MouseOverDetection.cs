using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace unicorn.GameStates
{
    [CreateAssetMenu(menuName = "Actions/MouseOverDetection")]
    public class MouseOverDetection : Action
    {
        public override void Execute(float d)
        {
            List<RaycastResult> results = Settings.GetUIObjs();

            IClickable c = null;

            foreach (RaycastResult r in results)
            {
                c = r.gameObject.GetComponentInParent<IClickable>();
                if (c != null)
                {
                    c.OnHighlight();
                    break;
                }
            }
        }
    }
}
