﻿using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Yarde.Observable;

namespace Yarde.Gameplay.Displayers
{
    public class ResourceDisplay : MonoBehaviour
    {
        [SerializeField] protected GameLoop gameLoop;
        [SerializeField] private ResourceType type;
        [SerializeField] private TextMeshProUGUI text;

        private float _previousValue;
        private string _format;

        private void Awake()
        {
            var value = type switch
            {
                ResourceType.Wood => gameLoop.State.Wood,
                ResourceType.Stone => gameLoop.State.Stone,
                ResourceType.Food => gameLoop.State.Food,
                _ => throw new ArgumentOutOfRangeException()
            };

            _format = type switch
            {
                ResourceType.Food => "F1",
                _ => ""
            };

            value.OnValueChanged += OnChanged;
            _previousValue = value.Value;
            OnChanged(value);
        }

        private void OnChanged(IObservableProperty<float> obj)
        {
            var roundedValue = obj.Value.ToString(_format);
            text.text = $"{roundedValue}";
            if (_previousValue < obj.Value)
            {
                AnimateGain().Forget();
            }
            else if (_previousValue > obj.Value)
            {
                AnimateLoss().Forget();
            }

            _previousValue = obj.Value;
        }

        private async UniTask AnimateGain()
        {
            await text.DOColor(Color.green, 0.2f);
            await text.DOColor(Color.white, 0.2f);
        }

        private async UniTask AnimateLoss()
        {
            await text.DOColor(Color.red, 0.2f);
            await text.DOColor(Color.white, 0.2f);
        }
    }

    public enum ResourceType
    {
        Wood,
        Stone,
        Food
    }
}