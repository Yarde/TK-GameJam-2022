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

        [SerializeField] protected AudioSource audioSource;
        [SerializeField] protected AudioClip clickClip;

        protected Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(StartAction);

            _gameLoop.State.IsBusy.OnValueChanged += SetButtonState;
            audioSource.clip = clickClip;
            audioSource.loop = true;

            Setup();
        }

        protected virtual void SetButtonState(IObservableProperty<bool> obj)
        {
            _button.interactable = !obj.Value;
        }

        private async void StartAction()
        {
            audioSource.Play();
            _gameLoop.State.IsBusy.Value = true;

            _loadingIcon.fillAmount = 0;

            await DoAction();

            audioSource.Stop();
            _gameLoop.State.IsBusy.Value = false;
        }

        protected abstract void Setup();
        protected abstract UniTask DoAction();
    }
}