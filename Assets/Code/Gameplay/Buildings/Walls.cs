using UnityEngine;
using Yarde.Gameplay.GameData;

namespace Yarde.Gameplay.Buildings
{
    public class Walls : Building
    {
        public Walls(GameState state) : base(state)
        {
            Data = Resources.Load<BuildingData>("Walls");
        }
    }
}