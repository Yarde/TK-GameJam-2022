using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
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
            OnActiveChange(_enemy.IsActive);
        }

        private void OnActiveChange(IObservableProperty<bool> obj)
        {
            _button.gameObject.SetActive(obj.Value);
        }

        protected override void Setup()
        {
        }

        protected override async UniTask DoAction()
        {
            _loadingIcon.DOFillAmount(1f, _data.TimeToKill).SetEase(Ease.Linear);
            await UniTask.Delay(_data.TimeToKill.ToMilliseconds());

            _enemy.Kill();
        }
    }
}