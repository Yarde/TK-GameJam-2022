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

        public void ShowMenu(string title, string description)
        {
            gameObject.SetActive(true);
            _title.text = title;
            _description.text = description;

            button.onClick.AddListener(() => SceneManager.LoadScene(0));
            buttonX.onClick.AddListener(() => SceneManager.LoadScene(0));
        }
    }
}