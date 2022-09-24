using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Yarde.Gameplay.GameplayButtons;
using Yarde.Observable;

namespace Yarde.Gameplay.Buildings
{
    public class BuildingView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameLoop gameLoop;
        [SerializeField] private BuildingType type;

        private Building _building;
        private int _previousLevel;

        private void Awake()
        {
            _building = gameLoop.State.Buildings[type];
            spriteRenderer.sprite = _building.Data.Sprites[_building.Level];

            _building.Level.OnValueChanged += OnLevelChanged;
            _previousLevel = _building.Level.Value;
        }

        private async void OnLevelChanged(IObservableProperty<int> obj)
        {
            if (_previousLevel > obj.Value)
            {
                await transform.DOShakePosition(0.2f, 2f);
                spriteRenderer.sprite = _building.Data.Sprites[_building.Level];
            }
            else
            {
                await spriteRenderer.DOFade(0f, 0.1f);
                spriteRenderer.sprite = _building.Data.Sprites[_building.Level];
                await spriteRenderer.DOFade(1f, 0.1f);
            }
        }
    }
}