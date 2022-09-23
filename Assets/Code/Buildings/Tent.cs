using UnityEngine;

namespace Yarde.Buildings
{
    public class Tent : Building
    {
        public Tent(GameState state) : base(state)
        {
            Data = Resources.Load<BuildingData>("Tent");
        }
    }
}