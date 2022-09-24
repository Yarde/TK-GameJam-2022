using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Yarde.Gameplay
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Button button;
        [SerializeField] private Button buttonX;
        [SerializeField] private GameLoop gameLoop;
        
        [SerializeField] private GameObject win;
        [SerializeField] private GameObject lossFood;
        [SerializeField] private GameObject lossWolf;

        public void ShowMenu(GameResult result)
        {
            switch (result)
            {
                case GameResult.LossFood:
                    _title.text = "You Lost!";
                    _description.text = $"You let your people starve to death. Make sure to feed them next time!. You managed to last {gameLoop.Cycles.Value} seconds";
                    break;
                case GameResult.LossWolf:
                    _title.text = "You Lost!";
                    _description.text = $"Your village is destroyed! Protect it with walls and fire next time. You managed to last {gameLoop.Cycles.Value} seconds";
                    break;
                case GameResult.Win:
                    _title.text = "You Won!";
                    _description.text = $"You managed to beat the game in {gameLoop.Cycles.Value} seconds";
                    break;
            }
            
            win.SetActive(result == GameResult.Win);
            lossFood.SetActive(result == GameResult.LossFood);
            lossWolf.SetActive(result == GameResult.LossWolf);

            gameObject.SetActive(true);
            button.onClick.AddListener(() => SceneManager.LoadScene(0));
            buttonX.onClick.AddListener(() => SceneManager.LoadScene(0));
        }
    }

    public enum GameResult
    {
        Win,
        LossFood,
        LossWolf
    }
}