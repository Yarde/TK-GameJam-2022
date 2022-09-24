using UnityEngine;
using Yarde.Observable;

namespace Yarde.Gameplay
{
    public class DayNightCycle : MonoBehaviour
    {
        [SerializeField] private GameLoop gameLoop;
        [SerializeField] private Light light;

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