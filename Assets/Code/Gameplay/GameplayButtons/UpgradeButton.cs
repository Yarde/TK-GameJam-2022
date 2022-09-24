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

        private void SetButtonActive()
        {
            if (_building.Data.MaxLevel <= _building.Level)
            {
                _button.gameObject.SetActive(false);
                return;
            }

            var cost = _building.Data.Costs[_building.Level + 1];
            _button.gameObject.SetActive(cost.HasEnough(_gameLoop.State));
        }

        protected override async UniTask DoAction()
        {
            _loadingIcon.DOFillAmount(1f, _building.Data.BuildTime).SetEase(Ease.Linear);
            await UniTask.Delay(_building.Data.BuildTime.ToMilliseconds());

            _building.Upgrade();
            _gameLoop.State.TakeResources(_building.Data.Costs[_building.Level]);
            SetButtonActive();
        }
    }
}