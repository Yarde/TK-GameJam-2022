using UnityEngine;
using VContainer;
using Yarde.Gameplay.GameData;
using Yarde.Observable;
using Yarde.Utils.Logger;

namespace Yarde.Gameplay
{
    public class GameLoop : MonoBehaviour
    {
        [Inject] private readonly GameState _gameState;

        private float _timer;
        private GameplayData _data;
        private ObservableProperty<int> _cycles;

        public GameState State => _gameState;
        public GameplayData Data => _data;
        public ObservableProperty<int> Cycles => _cycles;
        public float CurrentTime => Mathf.Cos((_cycles - _data.DayLength) / _data.DayLength) + _data.DayNightRatio;
        public bool IsNight => CurrentTime <= 0f;

        private void Awake()
        {
            _cycles = new(0, _gameState);
            _data = Resources.Load<GameplayData>("GameplayData");
        }

        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                return;
            }

            this.LogInfo($"Cycle: {_cycles.Value}, State: {_gameState}");
            _timer = _data.TickLenght;
            _cycles.Value++;

            if (_cycles.Value % _data.FoodLossCycles == 0)
            {
                _gameState.Food.Value -= _data.FoodLoss * (1 + _data.FoodLossModifier * _cycles.Value);
            }
            
        }
    }
}