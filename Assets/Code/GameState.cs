using System.Collections.Generic;
using Yarde.Buildings;
using Yarde.GameplayButtons;
using Yarde.Observable;

namespace Yarde
{
    public class GameState : ObservableState
    {
        public ObservableProperty<int> Wood { get; }
        public ObservableProperty<int> Stone { get; }
        public ObservableProperty<int> Food { get; }

        public Dictionary<BuildingType, Building> Buildings = new();

        public ObservableProperty<float> FireFuel { get; }

        public ObservableProperty<bool> IsBusy { get; }

        public GameState()
        {
            Wood = new ObservableProperty<int>(0, this);
            Stone = new ObservableProperty<int>(0, this);
            Food = new ObservableProperty<int>(0, this);

            Buildings[BuildingType.Tent] = new Tent(this);

            FireFuel = new ObservableProperty<float>(0, this);
            IsBusy = new ObservableProperty<bool>(false, this);
        }

        public override string ToString()
        {
            var result = "";
            result += $"Wood: {Wood.Value}, Stone: {Stone.Value}, Food: {Food.Value}, Fuel: {FireFuel.Value}, ";
            foreach (var building in Buildings)
            {
                result += $"{building.Key} level: {building.Value.Level.Value}, ";
            }

            return result;
        }

        public void TakeResources(ResourceCost dataCost)
        {
            Wood.Value -= dataCost.Wood;
            Stone.Value -= dataCost.Stone;
            Food.Value -= dataCost.Food;
        }
    }
}