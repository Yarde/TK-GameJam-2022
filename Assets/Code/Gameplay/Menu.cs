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

        public void ShowMenu(string title, string description)
        {
            gameObject.SetActive(true);
            _title.text = title;
            _description.text = description;

            button.onClick.AddListener(() => SceneManager.LoadScene(0));
        }
    }
}