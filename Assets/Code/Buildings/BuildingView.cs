using UnityEngine;
using UnityEngine.UI;
using Yarde.GameplayButtons;
using Yarde.Observable;

namespace Yarde.Buildings
{
    public class BuildingView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameLoop gameLoop;
        [SerializeField] private BuildingType type;

        private Building _building;

        private void Awake()
        {
            _building = gameLoop.State.Buildings[type];
            spriteRenderer.sprite = _building.Data.Sprites[_building.Level];

            _building.Level.OnValueChanged += OnLevelChanged;
        }

        private void OnLevelChanged(IObservableProperty<int> obj)
        {
            spriteRenderer.sprite = _building.Data.Sprites[_building.Level];
        }
    }
}