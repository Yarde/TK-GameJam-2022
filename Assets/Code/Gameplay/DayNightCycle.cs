using UnityEngine;
using UnityEngine.UI;
using Yarde.Observable;
using Yarde.Utils.Extensions;

namespace Yarde.Gameplay
{
    public class DayNightCycle : MonoBehaviour
    {
        [SerializeField] private GameLoop gameLoop;
        [SerializeField] private Light light;

        [SerializeField] private Image moon;
        [SerializeField] private Image moonFrame;
        [SerializeField] private Image sun;
        [SerializeField] private Image sunFrame;

        private void Update()
        {
            moon.SetAlpha(-gameLoop.CurrentTime);
            moonFrame.SetAlpha(-gameLoop.CurrentTime);
            sun.SetAlpha(gameLoop.CurrentTime);
            sunFrame.SetAlpha(gameLoop.CurrentTime);
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