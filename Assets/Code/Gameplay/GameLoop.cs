using System;
using UnityEngine;
using VContainer;
using Yarde.Gameplay.GameData;
using Yarde.Gameplay.GameplayButtons;
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
        
        private float DayNightRatio => Mathf.Max(0f, _data.DayNightRatio - 0.1f * _cycles.Value / Data.DayLength);
        public float CurrentTime => Mathf.Cos((_cycles.Value - _data.DayLength) / _data.DayLength) + DayNightRatio;
        public bool IsNight => CurrentTime <= 0f;

        public Action OnWin;
        public Action<GameResult> OnLoss;

        private void Awake()
        {
            _cycles = new(0, _gameState);
            _data = Resources.Load<GameplayData>("GameplayData");
        }

        private void Update()
        {
            var tent = _gameState.Buildings[BuildingType.Tent];
            if (_gameState.Food.Value < 0 || tent.Level.Value < 1 || tent.Level.Value == tent.Data.MaxLevel)
            {
                return;
            }
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
                if (_gameState.Food.Value < 0)
                {
                    OnLoss?.Invoke(GameResult.LossFood);
                }
            }
        }
    }
}