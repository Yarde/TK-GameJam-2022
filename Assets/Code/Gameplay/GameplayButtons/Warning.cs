using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Yarde.Gameplay;
using Yarde.Observable;

public class Warning : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Transform t;
    [SerializeField] private GameLoop gameLoop;
    private CancellationTokenSource _cancellation;
    private bool isAnimating;

    private void Start()
    {
        gameLoop.State.Food.OnValueChanged += OnFoodChange;
    }

    private void OnFoodChange(IObservableProperty<float> obj)
    {
        if (obj.Value < 10 && !isAnimating)
        {
            StartAnimating().Forget();
        }

        if (obj.Value >= 10 && isAnimating)
        {
            StopAnimating();
        }
    }

    private void StopAnimating()
    {
        isAnimating = false;
        _cancellation.Cancel();
        _cancellation.Dispose();
    }

    private async UniTask StartAnimating()
    {
        _cancellation = new CancellationTokenSource();
        isAnimating = true;

        while (!_cancellation.IsCancellationRequested)
        {
            t.DOScale(Vector3.one * 1.4f, 0.5f);
            await background.DOColor(Color.red, 0.5f);
            t.DOScale(Vector3.one, 0.5f);
            await background.DOColor(Color.white, 0.5f);

            await UniTask.Delay(500);
        }
    }
}