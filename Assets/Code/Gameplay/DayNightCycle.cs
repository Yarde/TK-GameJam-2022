using UnityEngine;
using UnityEngine.UI;
using Yarde.Observable;

namespace Yarde.Gameplay
{
    public class DayNightCycle : MonoBehaviour
    {
        [SerializeField] private GameLoop gameLoop;
        [SerializeField] private Light light;

        [SerializeField] private Image moon;
        [SerializeField] private Image sun;

        private void Update()
        {
            moon.enabled = gameLoop.IsNight;
            sun.enabled = !gameLoop.IsNight;
        }
        
        private void Start()
        {
            gameLoop.Cycles.OnValueChanged += OnCycleChange;
        }

        private void OnCycleChange(IObservableProperty<int> obj)
        {
            var intensity = Mathf.Cos((obj.Value - gameLoop.Data.DayLength) / gameLoop.Data.DayLength) +
                            gameLoop.Data.DayNightRatio;
            light.intensity = intensity;
        }
    }
}