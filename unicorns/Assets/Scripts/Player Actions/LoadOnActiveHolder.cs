﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unicorn
{
    [CreateAssetMenu(menuName = "Actions/Player Actions/LoadOnActiveHolder")]
    public class LoadOnActiveHolder : PlayerAction
    {
        public override void Execute(PlayerHolder player)
        {
            GameManager.singleton.LoadPlayerOnActive(player);
        }
    }
}