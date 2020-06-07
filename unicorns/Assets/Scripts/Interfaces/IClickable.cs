using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    public interface IClickable
    {
        void OnClick();

        void OnHighlight();
    }
}