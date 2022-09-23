using Yarde.Observable;

namespace Yarde
{
    public class GameState : ObservableState
    {
        public ObservableProperty<int> Wood { get; }
        public ObservableProperty<int> Stone { get; }
        public ObservableProperty<int> Food { get; }

        public ObservableProperty<int> TentLevel { get; }
        public ObservableProperty<int> FireplaceLevel { get; }
        public ObservableProperty<int> WallsLevel { get; }

        public ObservableProperty<float> FireFuel { get; }

        public GameState()
        {
            Wood = new ObservableProperty<int>(0, this);
            Stone = new ObservableProperty<int>(0, this);
            Food = new ObservableProperty<int>(0, this);

            TentLevel = new ObservableProperty<int>(0, this);
            FireplaceLevel = new ObservableProperty<int>(0, this);
            WallsLevel = new ObservableProperty<int>(0, this);

            FireFuel = new ObservableProperty<float>(0, this);
        }

        public override string ToString()
        {
            return $"Wood: {Wood.Value}, Stone: {Stone.Value}, Food: {Food.Value}, " +
                   $"Levels: {TentLevel.Value}, {FireplaceLevel.Value}, {WallsLevel.Value}, Fuel: {FireFuel.Value}";
        }
    }
}