﻿using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Yarde.Gameplay.Buildings;
using Yarde.Gameplay.GameData;
using Yarde.Observable;
using Yarde.Utils.Extensions;

namespace Yarde.Gameplay.GameplayButtons
{
    public class FireButton : ButtonBase
    {
        [SerializeField] private Light _light;
        private FireData _fireData;
        private Building _fireplace;
        private ObservableProperty<float> _fireFuel;

        [SerializeField] private GameObject particleObject;
        [SerializeField] private ParticleSystem[] fireParticle;

        protected override void Setup()
        {
            _fireData = Resources.Load<FireData>("FireData");
            _fireplace = _gameLoop.State.Buildings[BuildingType.Fireplace];
            _fireFuel = _gameLoop.State.FireFuel;

            _gameLoop.State.OnStateChanged += OnResourceChanged;
            _gameLoop.Cycles.OnValueChanged += OnTick;
            SetButtonActive();
        }

        private void OnTick(IObservableProperty<int> obj)
        {
            _fireFuel.Value -= _fireData.FuelLossOnTick;

            Tutorial();

            if (_fireFuel.Value <= 0f || _fireplace.Level < 1)
            {
                _fireFuel.Value = 0f;

                particleObject.SetActive(false);
                _light.gameObject.SetActive(false);
                return;
            }

            AnimateLight();
        }

        private void Tutorial()
        {
            if (_fireFuel.Value <= 0f && _fireplace.Level >= 1 && _gameLoop.IsNight)
            {
                transform.DOShakePosition(0.2f, Vector3.one * 3f, 1);
            }
        }

        private void AnimateLight()
        {
            particleObject.SetActive(true);
            _light.gameObject.SetActive(true);
            _light.DOIntensity(_fireFuel * _fireplace.Level, 0.2f);

            for (var index = 0; index < fireParticle.Length; index++)
            {
                var system = fireParticle[index];
                var main = system.main;
                var newSize = (_fireplace.Level.Value + 1) * (_fireFuel / _fireData.MaxFuel) * (index % 2 + 1) / 5;
                main.startLifetime = Mathf.Clamp(newSize, 0.2f * index % 2, 0.6f * index % 2);
            }
        }

        private void OnResourceChanged(IObservableState obj)
        {
            SetButtonActive();
        }

        private void SetButtonActive()
        {
            if (_fireplace.Level < 1)
            {
                _button.gameObject.SetActive(false);
                return;
            }

            if (_fireData.MaxFuel * _fireplace.Level <= _gameLoop.State.FireFuel.Value)
            {
                _button.gameObject.SetActive(false);
                return;
            }

            _button.gameObject.SetActive(_gameLoop.State.Wood >= _fireData.ActionWoodCost);
        }

        protected override async UniTask DoAction()
        {
            _loadingIcon.DOFillAmount(1f, _fireData.ActionTime).SetEase(Ease.Linear);
            await UniTask.Delay(_fireData.ActionTime.ToMilliseconds());

            _gameLoop.State.FireFuel.Value += _fireData.FuelGainOnAction;
            _gameLoop.State.Wood.Value -= _fireData.ActionWoodCost;
            OnTick(null);

            SetButtonActive();
        }
    }
}