using UnityEngine;
using Yarde.Gameplay.GameData;

namespace Yarde.Gameplay.Buildings
{
    public class Fireplace : Building
    {
        public Fireplace(GameState state) : base(state)
        {
            Data = Resources.Load<BuildingData>("Fireplace");
        }
    }
}