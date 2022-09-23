using UnityEngine;
using VContainer;
using Yarde.Observable;
using Yarde.Utils.Logger;

namespace Yarde
{
    public class GameLoop : MonoBehaviour
    {
        [Inject] private readonly GameState _gameState;

        public GameState State => _gameState;
        private float timer;
        private ObservableProperty<int> _cycles = new(0, null);
        public ObservableProperty<int> Cycles => _cycles;


        private void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                return;
            }

            this.LogInfo($"Cycle: {Cycles.Value}, State: {_gameState}");
            timer = 0.1f;
            _cycles.Value++;
        }
    }
}