using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Yarde.Gameplay.GameData;
using Yarde.Observable;
using Yarde.Utils.Extensions;

namespace Yarde.Gameplay.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public ObservableProperty<bool> IsActive { get; private set; }
        private EnemyData _data;
        private GameLoop _gameLoop;
        private CancellationTokenSource _cancellation;

        public void Setup(EnemyData data, GameLoop gameLoop)
        {
            _data = data;
            _gameLoop = gameLoop;
            spriteRenderer.enabled = false;
            IsActive = new ObservableProperty<bool>(false, gameLoop.State);
        }

        public void Show()
        {
            IsActive.Value = true;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            spriteRenderer.enabled = true;
            spriteRenderer.DOFade(1f, 0.3f);

            _cancellation = new CancellationTokenSource();
            StartAttack().Forget();
        }

        private async UniTask StartAttack()
        {
            await UniTask.Delay(_data.TimeToActivate.ToMilliseconds(), cancellationToken: _cancellation.Token);
            while (!_cancellation.IsCancellationRequested)
            {
                _gameLoop.State.Attack();
                transform.DOShakePosition(0.3f, 2f);
                await UniTask.Delay(_data.AttackSpeed.ToMilliseconds(), cancellationToken: _cancellation.Token);
            }
        }

        public void Kill()
        {
            IsActive.Value = false;

            spriteRenderer.enabled = false;

            _cancellation.Cancel();
            _cancellation.Dispose();
        }
    }
}