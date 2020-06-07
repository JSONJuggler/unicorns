using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    public class CardInstance : MonoBehaviour, IClickable
    {
        public void OnClick()
        {
            Debug.Log(this.gameObject.name);
        }

        public void OnHighlight()
        {
            Debug.Log(this.gameObject.name);
            Vector3 s = Vector3.one * 2;
            this.transform.localScale = s;
        }
    }
}