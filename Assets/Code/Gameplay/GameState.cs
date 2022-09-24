using System.Collections.Generic;
using UnityEngine;
using Yarde.Gameplay.Buildings;
using Yarde.Gameplay.GameData;
using Yarde.Gameplay.GameplayButtons;
using Yarde.Observable;

namespace Yarde.Gameplay
{
    public class GameState : ObservableState
    {
        public ObservableProperty<float> Wood { get; }
        public ObservableProperty<float> Stone { get; }
        public ObservableProperty<float> Food { get; }

        public Dictionary<BuildingType, Building> Buildings = new();

        public ObservableProperty<float> FireFuel { get; }

        public ObservableProperty<bool> IsBusy { get; }

        public GameState()
        {
            var resourceData = Resources.Load<GameplayData>("GameplayData");

            Wood = new ObservableProperty<float>(resourceData.WoodData.StartAmount, this);
            Stone = new ObservableProperty<float>(resourceData.StoneData.StartAmount, this);
            Food = new ObservableProperty<float>(resourceData.FoodData.StartAmount, this);

            Buildings[BuildingType.Tent] = new Tent(this);
            Buildings[BuildingType.Wall] = new Walls(this);
            Buildings[BuildingType.Fireplace] = new Fireplace(this);

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