using System;

namespace Yarde.Buildings
{
    [Serializable]
    public class ResourceCost
    {
        public int Wood;
        public int Stone;
        public int Food;

        public bool HasEnough(GameState state)
        {
            return Wood <= state.Wood && Stone <= state.Stone && Food <= state.Food;
        }
    }
}