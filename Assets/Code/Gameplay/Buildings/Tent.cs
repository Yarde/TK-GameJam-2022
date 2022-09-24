using UnityEngine;
using Yarde.Gameplay.GameData;

namespace Yarde.Gameplay.Buildings
{
    public class Tent : Building
    {
        public Tent(GameState state) : base(state)
        {
            Data = Resources.Load<BuildingData>("Tent");
        }
    }
}