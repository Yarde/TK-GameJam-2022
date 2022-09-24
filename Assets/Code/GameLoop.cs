using UnityEngine;
using VContainer;
using Yarde.GameData;
using Yarde.Observable;
using Yarde.Utils.Logger;

namespace Yarde
{
    public class GameLoop : MonoBehaviour
    {
        [Inject] private readonly GameState _gameState;

        private float _timer;
        private GameplayData _data;
        private ObservableProperty<int> _cycles = new(0, null);

        public GameState State => _gameState;
        public GameplayData Data => _data;
        public ObservableProperty<int> Cycles => _cycles;

        private void Awake()
        {
            _data = Resources.Load<GameplayData>("GameplayData");
        }

        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                return;
            }

            this.LogInfo($"Cycle: {Cycles.Value}, State: {_gameState}");
            _timer = _data.TickLenght;
            _cycles.Value++;
        }
    }
}