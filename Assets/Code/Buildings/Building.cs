using Yarde.GameData;
using Yarde.Observable;
using Yarde.Utils.Logger;

namespace Yarde.Buildings
{
    public abstract class Building
    {
        public ObservableProperty<int> Level { get; }
        public BuildingData Data;

        protected Building(GameState state)
        {
            Level = new ObservableProperty<int>(0, state);
        }

        public void Upgrade()
        {
            if (Level >= Data.MaxLevel)
            {
                return;
            }
            Level.Value++;
        }
        
        public void Downgrade()
        {
            if (Level == 0)
            {
                this.LogError("Game lost!");
            }
            Level.Value--;
        }
    }
}