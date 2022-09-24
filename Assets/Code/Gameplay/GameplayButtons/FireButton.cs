using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Yarde.Gameplay.GameData;
using Yarde.Observable;
using Yarde.Utils.Extensions;

namespace Yarde.Gameplay.GameplayButtons
{
    public class FireButton : ButtonBase
    {
        [SerializeField] private Light _light;
        private FireData _fireData;
        private ObservableProperty<float> _fireFuel;

        protected override void Setup()
        {
            _fireData = Resources.Load<FireData>("FireData");
            _fireFuel = _gameLoop.State.FireFuel;

            _gameLoop.State.Wood.OnValueChanged += OnResourceChanged;
            _gameLoop.Cycles.OnValueChanged += OnTick;
            SetButtonActive();
        }

        private void OnTick(IObservableProperty<int> obj)
        {
            _fireFuel.Value -= _fireData.FuelLossOnTick;
            _light.intensity = _fireFuel;
        }

        private void OnResourceChanged(IObservableProperty<float> obj)
        {
            SetButtonActive();
        }

        private void SetButtonActive()
        {
            if (_fireData.MaxFuel <= _gameLoop.State.FireFuel)
            {
                _button.gameObject.SetActive(false);
                return;
            }

            _button.gameObject.SetActive(_gameLoop.State.Wood >= _fireData.ActionWoodCost);
        }

        protected override async UniTask DoAction()
        {
            _loadingIcon.DOFillAmount(1f, _fireData.ActionTime);
            await UniTask.Delay(_fireData.ActionTime.ToMilliseconds());

            _gameLoop.State.FireFuel.Value += 10;
            _gameLoop.State.Wood.Value -= _fireData.ActionWoodCost;

            SetButtonActive();
        }
    }
}