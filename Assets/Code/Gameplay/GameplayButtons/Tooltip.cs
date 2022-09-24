using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Yarde.Gameplay.GameplayButtons
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private Image woodImage;
        [SerializeField] private Image stoneImage;
        [SerializeField] private TextMeshProUGUI woodText;
        [SerializeField] private TextMeshProUGUI stoneText;

        public void SetPrice(float wood, float stone)
        {
            woodImage.gameObject.SetActive(wood > 0);
            woodText.gameObject.SetActive(wood > 0);
            woodText.text = wood.ToString();

            stoneImage.gameObject.SetActive(stone > 0);
            stoneText.gameObject.SetActive(stone > 0);
            stoneText.text = stone.ToString();
        }
    }
}