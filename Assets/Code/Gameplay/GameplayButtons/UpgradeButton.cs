using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Yarde.Gameplay.Buildings;
using Yarde.Observable;
using Yarde.Utils.Extensions;

namespace Yarde.Gameplay.GameplayButtons
{
    public class UpgradeButton : ButtonBase
    {
        [SerializeField] private BuildingType type;
        [SerializeField] private Tooltip tooltip;

        private Building _building;

        protected override void Setup()
        {
            _building = _gameLoop.State.Buildings[type];
            _gameLoop.State.OnStateChanged += OnResourceChanged;
            SetButtonActive();
        }

        private void OnResourceChanged(IObservableState obj)
        {
            SetButtonActive();
        }

        protected override void SetButtonState(IObservableProperty<bool> obj)
        {
        }

        private void SetButtonActive()
        {
            if (_building.Data.MaxLevel <= _building.Level)
            {
                _button.gameObject.SetActive(false);
                tooltip.gameObject.SetActive(false);
                return;
            }

            var cost = _building.Data.Costs[_building.Level + 1];
            _button.interactable = cost.HasEnough(_gameLoop.State) && !_gameLoop.State.IsBusy.Value;
            _button.gameObject.SetActive(true);
            tooltip.gameObject.SetActive(true);
            tooltip.SetPrice(cost.Wood, cost.Stone);
        }

        protected override async UniTask DoAction()
        {
            _loadingIcon.DOFillAmount(1f, _building.Data.BuildTime).SetEase(Ease.Linear);
            await UniTask.Delay(_building.Data.BuildTime.ToMilliseconds());

            _building.Upgrade(_gameLoop);
            _gameLoop.State.TakeResources(_building.Data.Costs[_building.Level]);
        }
    }
}