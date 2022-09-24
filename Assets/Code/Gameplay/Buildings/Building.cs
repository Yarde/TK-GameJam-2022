using Yarde.Gameplay.GameData;
using Yarde.Gameplay.GameplayButtons;
using Yarde.Observable;

namespace Yarde.Gameplay.Buildings
{
    public abstract class Building
    {
        public ObservableProperty<int> Level { get; }
        public ObservableProperty<int> HealthPoints { get; }
        public BuildingData Data;
        protected GameState _state;

        protected Building(GameState state)
        {
            _state = state;
            Level = new ObservableProperty<int>(0, state);
            HealthPoints = new ObservableProperty<int>(3, state);
        }

        public void Upgrade(GameLoop gameLoop)
        {
            if (Level >= Data.MaxLevel)
            {
                return;
            }

            Level.Value++;
            HealthPoints.Value = Data.HealthPoints * Level.Value;

            if (_state.Buildings[BuildingType.Tent].Level.Value == _state.Buildings[BuildingType.Tent].Data.MaxLevel)
            {
                gameLoop.OnWin.Invoke();
            }
        }

        public virtual void Downgrade(GameLoop gameLoop)
        {
            HealthPoints.Value--;
            if (HealthPoints.Value <= 0)
            {
                Level.Value = 0;
            }
        }
    }
}