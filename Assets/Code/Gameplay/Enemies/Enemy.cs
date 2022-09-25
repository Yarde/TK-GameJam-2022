using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Yarde.Gameplay.GameData;
using Yarde.Gameplay.GameplayButtons;
using Yarde.Observable;
using Yarde.Utils.Extensions;

namespace Yarde.Gameplay.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip onSpawn;
        [SerializeField] private AudioClip onAttack;
        [SerializeField] private AudioClip onDead;

        [SerializeField] private Vector3 start;
        [SerializeField] private Vector3 enter;
        [SerializeField] private Vector3 middle = new(0f, 0f, -6f);

        [SerializeField] private ParticleSystem wallParticle;
        [SerializeField] private ParticleSystem tentParticle;

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
            transform.position = start;
            transform.DOMove(enter, 1f);

            IsActive.Value = true;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            spriteRenderer.enabled = true;
            spriteRenderer.DOFade(1f, 0.3f);

            source.clip = onSpawn;
            source.Play();

            _cancellation = new CancellationTokenSource();
            StartAttack().Forget();
        }

        private async UniTask StartAttack()
        {
            var timeToFirstAttach =
                Mathf.Max(_data.TimeToActivate - _gameLoop.Cycles.Value / (_gameLoop.Data.DayLength * 2f),
                    _data.TimeToActivate / 2f);
            await UniTask.Delay(timeToFirstAttach.ToMilliseconds(), cancellationToken: _cancellation.Token);
            while (!_cancellation.IsCancellationRequested)
            {
                _gameLoop.State.Attack(_gameLoop);
                if (_gameLoop.State.Buildings[BuildingType.Wall].Level > 0)
                {
                    wallParticle.Play();
                }
                else if (_gameLoop.State.Buildings[BuildingType.Tent].Level > 0)
                {
                    tentParticle.Play();
                }

                source.clip = onAttack;
                source.Play();
                transform.DOShakePosition(0.3f, 2f);
                await transform.DOMove(middle, 0.4f).SetEase(Ease.InCubic).WithCancellation(_cancellation.Token);
                if (_cancellation.IsCancellationRequested) return;
                transform.DOMove(enter, 0.4f).SetEase(Ease.OutCubic);

                var attachSpeed = _data.AttackSpeed +
                                  Random.Range(
                                      Mathf.Max(-1, -(_gameLoop.Cycles.Value / (_gameLoop.Data.DayLength * 2))), 1f);
                await UniTask.Delay(attachSpeed.ToMilliseconds(), cancellationToken: _cancellation.Token);
            }
        }

        public void Kill()
        {
            IsActive.Value = false;

            spriteRenderer.enabled = false;
            source.clip = onDead;
            source.Play();

            _cancellation.Cancel();
            _cancellation.Dispose();
        }
    }
}