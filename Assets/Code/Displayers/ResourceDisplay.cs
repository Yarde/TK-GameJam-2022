using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Yarde.Observable;

namespace Yarde.Displayers
{
    public class ResourceDisplay : MonoBehaviour
    {
        [SerializeField] protected GameLoop gameLoop;
        [SerializeField] private ResourceType type;

        private TextMeshProUGUI _text;
        private int _previousValue;
        private ObservableProperty<int> _value;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();

            _value = type switch
            {
                ResourceType.Wood => gameLoop.State.Wood,
                ResourceType.Stone => gameLoop.State.Stone,
                ResourceType.Food => gameLoop.State.Food,
                _ => throw new ArgumentOutOfRangeException()
            };

            _value.OnValueChanged += OnChanged;
            _previousValue = _value.Value;
        }

        private void OnChanged(IObservableProperty<int> obj)
        {
            _text.text = $"{type}: {_value.Value}";
            if (_previousValue < _value.Value)
            {
                AnimateGain().Forget();
            }
            else
            {
                AnimateLoss().Forget();
            }

            _previousValue = _value.Value;
        }

        private async UniTask AnimateGain()
        {
            await _text.DOColor(Color.green, 0.2f);
            await _text.DOColor(Color.white, 0.2f);
        }

        private async UniTask AnimateLoss()
        {
            await _text.DOColor(Color.red, 0.2f);
            await _text.DOColor(Color.white, 0.2f);
        }
    }

    public enum ResourceType
    {
        Wood,
        Stone,
        Food
    }
}