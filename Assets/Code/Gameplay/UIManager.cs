using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Yarde.Gameplay
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameLoop _gameLoop;
        [SerializeField] private Menu _menu;
        [SerializeField] private GameObject _gameplayUI;
        [SerializeField] private GraphicRaycaster raycaster;

        private void Start()
        {
            _gameplayUI.SetActive(true);
            _menu.gameObject.SetActive(false);
            _gameLoop.OnWin += OnWin;
            _gameLoop.OnLoss += OnLoss;
        }

        private async void OnWin()
        {
            raycaster.enabled = false;
            await UniTask.Delay(2000);
            _gameplayUI.SetActive(false);
            _menu.gameObject.SetActive(true);
            _menu.ShowMenu(GameResult.Win);
            raycaster.enabled = true;
        }

        private async void OnLoss(GameResult reason)
        {
            raycaster.enabled = false;
            await UniTask.Delay(2000);
            _gameplayUI.SetActive(false);
            _menu.gameObject.SetActive(true);
            _menu.ShowMenu(reason);
            raycaster.enabled = true;
        }
    }
}