using UnityEngine;
using UnityEngine.UI;
using Yarde.Gameplay.Buildings;

namespace Yarde.Gameplay.GameplayButtons
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private GameLoop gameLoop;
        [SerializeField] private Slider slider;
        [SerializeField] private BuildingType type;

        private Building _building;

        private void Start()
        {
            slider.interactable = false;
            _building = gameLoop.State.Buildings[type];

            _building.HealthPoints.OnValueChanged += _ => OnHPChange();
            _building.Level.OnValueChanged += _ => OnHPChange();
            OnHPChange();
        }

        private void OnHPChange()
        {
            slider.gameObject.SetActive(_building.Level.Value >= 1);
            slider.value = _building.HealthPoints.Value / (float)(_building.Data.HealthPoints * _building.Data.MaxLevel);
        }
    }
}