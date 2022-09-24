using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Yarde.Gameplay.Displayers;
using Yarde.Gameplay.GameData;
using Yarde.Observable;
using Yarde.Utils.Extensions;

namespace Yarde.Gameplay.GameplayButtons
{
    public class ActionButton : ButtonBase
    {
        [SerializeField] private ResourceType type;

        private ObservableProperty<float> _value;
        private ResourceData _data;

        protected override void Setup()
        {
            _value = type switch
            {
                ResourceType.Wood => _gameLoop.State.Wood,
                ResourceType.Food => _gameLoop.State.Food,
                ResourceType.Stone => _gameLoop.State.Stone,
                _ => throw new ArgumentOutOfRangeException()
            };

            _data = type switch
            {
                ResourceType.Wood => _gameLoop.Data.WoodData,
                ResourceType.Food => _gameLoop.Data.FoodData,
                ResourceType.Stone => _gameLoop.Data.StoneData,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override async UniTask DoAction()
        {
            _loadingIcon.DOFillAmount(1f, _data.TimeToCollect);
            await UniTask.Delay(_data.TimeToCollect.ToMilliseconds());
            _value.Value += (int)_data.Gain;
        }
    }
}