using System.Linq;
using Yarde.Gameplay.GameData;
using Yarde.Observable;

namespace Yarde.Gameplay.Buildings
{
    public abstract class Building
    {
        public ObservableProperty<int> Level { get; }
        public BuildingData Data;
        private GameState _state;

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

            if (_state.Buildings.All(x => x.Value.Level == x.Value.Data.MaxLevel))
            {
                gameLoop.OnWin.Invoke();
            }
        }

        public virtual void Downgrade(GameLoop gameLoop)
        {
            Level.Value--;
        }
    }
}