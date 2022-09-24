using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Yarde.Observable;

namespace Yarde.Gameplay.GameplayButtons
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonBase : MonoBehaviour
    {
        [SerializeField] protected GameLoop _gameLoop;
        [SerializeField] protected Image _loadingIcon;
        
        protected Button _button;
        
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(StartAction);

            _gameLoop.State.IsBusy.OnValueChanged += SetButtonState;

            _loadingIcon.enabled = false;

            Setup();
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

            await DoAction();

            _loadingIcon.enabled = false;
            _gameLoop.State.IsBusy.Value = false;
        }

        protected abstract void Setup();
        protected abstract UniTask DoAction();
    }
}