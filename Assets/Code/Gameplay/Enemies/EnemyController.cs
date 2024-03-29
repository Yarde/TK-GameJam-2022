﻿using UnityEngine;
using Yarde.Gameplay.GameData;
using Yarde.Gameplay.GameplayButtons;
using Yarde.Observable;
using Yarde.Utils.Logger;

namespace Yarde.Gameplay.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private GameLoop _gameLoop;

        [SerializeField] private EnemyData[] allData;
        [SerializeField] private Enemy[] enemies;
        [SerializeField] private EnemyButton[] buttons;

        private float _chance;

        private void Start()
        {
            _gameLoop.Cycles.OnValueChanged += OnTick;

            for (var index = 0; index < enemies.Length; index++)
            {
                var enemy = enemies[index];
                var data = allData[index];
                enemy.Setup(data, _gameLoop);
                buttons[index].SetData(enemy, data);
            }
        }

        private void OnTick(IObservableProperty<int> obj)
        {
            var threat = 0f;
            var maxThreat = (0f - _gameLoop.CurrentTime) * (_gameLoop.Cycles.Value / _gameLoop.Data.DayLength / 3f);

            for (var i = 0; i < enemies.Length; i++)
            {
                var enemy = enemies[i];
                var data = allData[i];

                this.LogInfo(
                    $"Time: {_gameLoop.CurrentTime}, Max threat: {maxThreat}, threat: {threat}, {enemy.IsActive.Value}, {data.ThresholdToActivate}");

                if (enemy.IsActive.Value)
                {
                    threat += data.Threat;
                }
                else if (threat < maxThreat && threat < data.ThresholdToActivate)
                {
                    var fireModifier = _gameLoop.State.FireFuel.Value;
                    var difficultyProgression = _gameLoop.Data.AttacksModifier * _gameLoop.Cycles.Value;
                    var probabilityOfAttack = _chance - fireModifier + difficultyProgression;
                    var random = Random.Range(data.ChanceOfOccurence, 100f);

                    this.LogInfo($"random: {random} < {_chance} - {fireModifier} + {difficultyProgression}");
                    if (random < probabilityOfAttack)
                    {
                        enemy.Show();
                        _chance = 0;
                    }
                    else
                    {
                        _chance += 1f;
                    }
                }
            }
        }
    }
}