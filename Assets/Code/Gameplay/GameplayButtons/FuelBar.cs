using UnityEngine;
using UnityEngine.UI;
using Yarde.Gameplay.Buildings;
using Yarde.Gameplay.GameData;
using Yarde.Utils.Logger;

namespace Yarde.Gameplay.GameplayButtons
{
    public class FuelBar : MonoBehaviour
    {
        [SerializeField] private GameLoop gameLoop;
        [SerializeField] private Slider slider;

        private Building _fireplace;
        private FireData _fireData;

        private void Start()
        {
            slider.interactable = false;

            _fireplace = gameLoop.State.Buildings[BuildingType.Fireplace];
            _fireData = Resources.Load<FireData>("FireData");

            gameLoop.State.FireFuel.OnValueChanged += _ => OnFuelChange();
            OnFuelChange();
        }

        private void OnFuelChange()
        {
            slider.gameObject.SetActive(_fireplace.Level.Value >= 1);
            if (_fireplace.Level.Value >= 1)
            {
                slider.value = gameLoop.State.FireFuel.Value / (_fireData.MaxFuel * _fireplace.Level.Value);
            }
        }
    }
}