﻿using UnityEngine;
using VContainer;
using Yarde.Utils.Logger;

namespace Yarde
{
    public class GameLoop : MonoBehaviour
    {
        [Inject] private readonly GameState _gameState;

        public GameState State => _gameState;
        private float timer;

        private void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                return;
            }

            this.LogInfo(_gameState.ToString());
            timer = 1;
        }
    }
}