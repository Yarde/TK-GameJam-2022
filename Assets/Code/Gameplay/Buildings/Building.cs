using Yarde.Gameplay.GameData;
using Yarde.Observable;
using Yarde.Utils.Logger;

namespace Yarde.Gameplay.Buildings
{
    public abstract class Building
    {
        public ObservableProperty<int> Level { get; }
        public BuildingData Data;

        protected Building(GameState state)
        {
            Level = new ObservableProperty<int>(0, state);
        }

        public virtual void Upgrade(GameLoop gameLoop)
        {
            if (Level >= Data.MaxLevel)
            {
                return;
            }

            Level.Value++;
        }

        public virtual void Downgrade(GameLoop gameLoop)
        {
            Level.Value--;
        }
    }
}