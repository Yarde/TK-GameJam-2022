using UnityEngine;
using Yarde.Gameplay.GameData;
using Yarde.Observable;

namespace Yarde.Gameplay.Buildings
{
    public class Walls : Building
    {
        public Walls(GameState state) : base(state)
        {
            Data = Resources.Load<BuildingData>("Walls");
            Level.Value = 1;
            HealthPoints = new ObservableProperty<int>(Data.HealthPoints, state);
        }
    }
}