using Cysharp.Threading.Tasks;
using DG.Tweening;
using Yarde.Gameplay.Enemies;
using Yarde.Gameplay.GameData;
using Yarde.Observable;
using Yarde.Utils.Extensions;

namespace Yarde.Gameplay.GameplayButtons
{
    public class EnemyButton : ButtonBase
    {
        private ObservableProperty<float> _value;
        private EnemyData _data;
        private Enemy _enemy;

        public void SetData(Enemy enemy, EnemyData data)
        {
            _data = data;
            _enemy = enemy;

            _enemy.IsActive.OnValueChanged += OnActiveChange;
            //_gameLoop.State.IsBusy.OnValueChanged += OnActiveChange;
            OnActiveChange(_enemy.IsActive);
        }

        private void OnActiveChange(IObservableProperty<bool> obj)
        {
            if (!_button)
            {
                return;
            }

            _button.gameObject.SetActive(_enemy.IsActive);
        }

        protected override void Setup()
        {
            if (_enemy)
            {
                OnActiveChange(_enemy.IsActive);
            }
        }

        protected override async UniTask DoAction()
        {
            _loadingIcon.DOFillAmount(1f, _data.TimeToKill).SetEase(Ease.Linear);
            await UniTask.Delay(_data.TimeToKill.ToMilliseconds());

            _enemy.Kill();
        }
    }
}