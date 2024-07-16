using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay.Player;
using Loaders;
using UnityEngine;

namespace GamePlay.Player
{
    public class EnergyUI : LoaderUI
    {
        public override void Init(int total)
        {
            base.Init(total);
            curProgress = 100;
            UpdateUI();
        }

        public void SetEnergyInPlayer(PlayerController player)
        {
            player.SetEnergyUI(this);
        }

        public void RemoveProgress(int progress)
        {
            curProgress -= progress;
            if (curProgress < 0)
            {
                curProgress = 0;
            }

            UpdateUI();
        }

        public override void AddProgress(int progress)
        {
            curProgress += progress;
            if (curProgress > 100)
            {
                curProgress = 100;
            }

            UpdateUI();
        }
    }
}