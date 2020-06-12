using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace unicorn
{
    [CreateAssetMenu]
    public class MainDataHolder : ScriptableObject
    {
        public GameElements.GE_Logic handLogic;
        public GameElements.GE_Logic cardDownLogic;
        public GameObject cardPrefab;
    }
}