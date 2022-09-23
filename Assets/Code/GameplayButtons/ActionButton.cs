using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Yarde.Observable;
using Yarde.Utils.Extensions;

namespace Yarde.GameplayButtons
{
    [RequireComponent(typeof(Button))]
    public abstract class ActionButton : MonoBehaviour
    {
        [SerializeField] protected GameLoop _gameLoop;
        [SerializeField] private Image _loadingIcon;
        [SerializeField] private float _durationInSec;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(StartAction);

            _gameLoop.State.IsBusy.OnValueChanged += SetButtonState;

            _loadingIcon.enabled = false;
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

            DoAction();

            _loadingIcon.enabled = false;
            _gameLoop.State.IsBusy.Value = false;
        }

        protected abstract void DoAction();
    }
}