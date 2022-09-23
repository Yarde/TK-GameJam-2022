using UnityEngine;
using Yarde.Observable;

namespace Yarde
{
    public class DayNightCycle : MonoBehaviour
    {
        [SerializeField] private GameLoop gameLoop;
        [SerializeField] private Light light;
        [SerializeField] private float daySpeed = 100f;

        private void Awake()
        {
            gameLoop.Cycles.OnValueChanged += OnCycleChange;
        }

        private void OnCycleChange(IObservableProperty<int> obj)
        {
            var intensity = Mathf.Cos((obj.Value - 100) / daySpeed) + 0.7f;
            light.intensity = intensity;
        }
    }
}