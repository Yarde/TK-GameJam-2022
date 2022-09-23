using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Yarde.Buildings;
using Yarde.Observable;
using Yarde.Utils.Extensions;

namespace Yarde.GameplayButtons
{
    [RequireComponent(typeof(Button))]
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] protected GameLoop _gameLoop;
        [SerializeField] private Image _loadingIcon;
        [SerializeField] private float _durationInSec;
        [SerializeField] private BuildingType type;

        private Button _button;
        private Building _building;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(StartAction);

            _building = _gameLoop.State.Buildings[type];

            _gameLoop.State.IsBusy.OnValueChanged += SetButtonState;
            _gameLoop.State.OnStateChanged += OnResourceChanged;

            _loadingIcon.enabled = false;
            SetButtonActive();
        }

        private void OnResourceChanged(IObservableState obj)
        {
            SetButtonActive();
        }

        private void SetButtonActive()
        {
            var cost = _building.Data.Costs[_building.Level + 1];

            if (cost.Wood <= _gameLoop.State.Wood
                && cost.Stone <= _gameLoop.State.Stone
                && cost.Food <= _gameLoop.State.Food
                && _building.Data.MaxLevel > _building.Level)
            {
                _button.gameObject.SetActive(true);
            }
            else
            {
                _button.gameObject.SetActive(false);
            }
        }

        private void SetButtonState(IObservableProperty<bool> obj)
        {
            _button.interactable = !obj.Value;
        }

        private async void StartAction()
        {
            _gameLoop.State.IsBusy.Value = true;

            _loadingIcon.enabled = true;
            _loadingIcon.fillAmount = 0;
            _loadingIcon.DOFillAmount(1f, _durationInSec);
            await UniTask.Delay(_durationInSec.ToMilliseconds());

            _building.Upgrade();
            _gameLoop.State.TakeResources(_building.Data.Costs[_building.Level]);

            _loadingIcon.enabled = false;
            _gameLoop.State.IsBusy.Value = false;
            SetButtonActive();
        }
    }
}