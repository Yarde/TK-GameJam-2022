using Yarde.Gameplay.GameData;
using Yarde.Gameplay.GameplayButtons;
using Yarde.Observable;
using Yarde.Utils.Logger;

namespace Yarde.Gameplay.Buildings
{
    public abstract class Building
    {
        public ObservableProperty<int> Level { get; }
        public ObservableProperty<int> HealthPoints { get; set; }
        public BuildingData Data;
        protected GameState _state;

        protected Building(GameState state)
        {
            _state = state;
            Level = new ObservableProperty<int>(0, state);
        }

        public void Upgrade(GameLoop gameLoop)
        {
            if (Level >= Data.MaxLevel)
            {
                return;
            }

            Level.Value++;
            if (HealthPoints != null)
            {
                HealthPoints.Value = Data.HealthPoints * Level.Value;
            }
            
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
                Level.Value--;
            }
            this.LogWarning($"{GetType()} got damaged, {HealthPoints.Value}, {Level.Value}");
        }
    }
}