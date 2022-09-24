using System;
using UnityEngine;

namespace Yarde.Gameplay
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameLoop _gameLoop;
        [SerializeField] private Menu _menu;
        [SerializeField] private GameObject _gameplayUI;

        private void Start()
        {
            _gameplayUI.SetActive(true);
            _menu.gameObject.SetActive(false);
            _gameLoop.OnWin += OnWin;
            _gameLoop.OnLoss += OnLoss;
        }

        private void OnWin()
        {
            _gameplayUI.SetActive(false);
            _menu.gameObject.SetActive(true);
            _menu.ShowMenu("You Won!", $"You managed to beat the game in {_gameLoop.Cycles.Value} seconds");
        }
        
        private void OnLoss(string reason)
        {
            _gameplayUI.SetActive(false);
            _menu.gameObject.SetActive(true);
            _menu.ShowMenu("You Lost!", $"{reason}. You managed to last {_gameLoop.Cycles.Value} seconds");
        }
    }
}