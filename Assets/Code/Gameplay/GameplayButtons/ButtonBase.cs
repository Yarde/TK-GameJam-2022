using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Yarde.Observable;

namespace Yarde.Gameplay.GameplayButtons
{
    public abstract class ButtonBase : MonoBehaviour
    {
        [SerializeField] protected GameLoop _gameLoop;
        [SerializeField] protected Image _loadingIcon;

        [SerializeField] protected AudioSource audioSource;
        [SerializeField] protected AudioClip clickClip;
        [SerializeField] protected GameObject human;
        [SerializeField] protected Vector3 position;
        [SerializeField] protected Vector3 defaultPosition;
        [SerializeField] protected bool flip;

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

            human.transform.position = position;
            human.transform.localScale = new Vector3(flip ? -1f : 1f, 1f, 1f);

            //human.SetActive(true);

            await DoAction();

            human.transform.position = defaultPosition;
            human.transform.localScale = new Vector3(1f, 1f, 1f);
            
            //human.SetActive(false);

            audioSource.Stop();
            _gameLoop.State.IsBusy.Value = false;
        }

        protected abstract void Setup();
        protected abstract UniTask DoAction();
    }
}